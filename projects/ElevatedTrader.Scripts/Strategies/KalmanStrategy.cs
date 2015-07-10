using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;
using MathNet.Filtering.Kalman;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Kalman
{
	public class Indicator : IIndicator
	{
		private List<IIndicatorResult> results;
		private DiscreteKalmanFilter kalman;
		private Matrix<double> F, G, Q, H, R;

		public IList<IIndicatorResult> Results
		{
			get { return results; }
		}

		public PeriodValueType PeriodValue
		{
			get;
			set;
		}

		public double? PlantNoise
		{
			get;
			set;
		}

		public double? MeasurementNoise
		{
			get;
			set;
		}

		public bool IsStockPriceRelated
		{
			get { return true; }
		}

		public Indicator(int capacity)
		{
			results = new List<IIndicatorResult>(capacity);

			AfterNewPeriod();
		}

		public bool SmoothingEnabled
		{
			get;
			set;
		}

		public void Calculate(IList<ITradingPeriod> periods)
		{
			if (periods.Count < 2) return;

			var current = periods[periods.Count - 1];
			var current_price = GetPeriodPrice(current);
			var previous = periods[periods.Count - 2];
			var previous_price = GetPeriodPrice(previous);

			if (kalman == null)
			{
				var measurement_noise = GetMeasurementNoise(current);
				var plant_noise = GetPlantNoise(current);
				var velocity = current_price - previous_price;
				var variance = current.High - current.Low;
				//var interval = previous.TickCount;
				var interval = 1;

				var x0 = Matrix<double>.Build.Dense(2, 1, new[] { current_price, velocity / interval });
				var p0 = Matrix<double>.Build.Dense(2, 2, new[] { measurement_noise, measurement_noise / interval, measurement_noise / interval, 2 * measurement_noise / (interval * interval) });

				F = Matrix<double>.Build.Dense(2, 2, new[] { 1d, 0d, interval, 1 });   // State transition matrix
				G = Matrix<double>.Build.Dense(2, 1, new[] { (interval * interval) / 2d, interval });   // Plant noise matrix
				Q = Matrix<double>.Build.Dense(1, 1, new[] { plant_noise }); // Plant noise variance

				H = Matrix<double>.Build.Dense(1, 2, new[] { 1d, 0d }); // Measurement matrix
				R = Matrix<double>.Build.Dense(1, 1, new[] { measurement_noise }); // Measurement variance matrix

				kalman = new DiscreteKalmanFilter(x0, p0);
				kalman.Predict(F, G, Q);

				return;
			}

			PerformKalman(current);

			var prediction = kalman.State[0, 0];

			var result = (IndicatorResult)Results[Results.Count - 1];
			result.Values.Clear();
			result.Values.Add(prediction);

			if (Results.Count > 1)
			{
				var last = Results[Results.Count - 2];

				if (last.Values.Count > 0)
				{
					if (prediction == last.Values[0])
					{
						result.Direction = TrendDirection.Sideways;
					}
					else
					{
						result.Direction = prediction > last.Values[0]
							? TrendDirection.Rising
							: TrendDirection.Falling;
					}
				}

				result.Signaled = last.Direction != TrendDirection.None && last.Direction != result.Direction;
			}
		}

		private double GetMeasurementNoise(ITradingPeriod current)
		{
			var noise = current.PeriodValue(PeriodValueType.Variance);

			return MeasurementNoise.HasValue ? (SmoothingEnabled ? noise * MeasurementNoise.Value : MeasurementNoise.Value) : noise;
		}

		private double GetPlantNoise(ITradingPeriod current)
		{
			var noise = current.PeriodValue(PeriodValueType.HarmonicMean);

			return PlantNoise.HasValue ? (SmoothingEnabled ? noise * PlantNoise.Value : PlantNoise.Value) : noise;
		}

		private double GetPeriodPrice(ITradingPeriod current)
		{
			return current.PeriodValue(PeriodValue);
		}

		private void PerformKalman(ITradingPeriod current)
		{
			var current_price = GetPeriodPrice(current);
			var measurement = Matrix<double>.Build.Dense(1, 1, new[] { current_price });
			R[0, 0] = GetMeasurementNoise(current);
			kalman.Update(measurement, H, R);

			Q[0, 0] = GetPlantNoise(current);
			kalman.Predict(F, G, Q);
		}

		public void BeforeNewPeriod(ITradingPeriod current)
		{
			if (kalman != null)
			{
				PerformKalman(current);
			}
		}

		public void AfterNewPeriod()
		{
			Results.Add(new IndicatorResult());
		}
	}

	public class Settings : TradingStrategySettings
	{
		private double? plantNoise = 0.1;
		private double? measurementNoise = null;
		private bool smoothingEnabled = false;
		private PeriodValueType smoothingValue = PeriodValueType.Close;

		public bool SmoothingEnabled
		{
			get { return smoothingEnabled; }
			set { smoothingEnabled = value; }
		}

		public double? PlantNoise
		{
			get { return plantNoise; }
			set { plantNoise = value; }
		}

		public double? MeasurementNoise
		{
			get { return measurementNoise; }
			set { measurementNoise = value; }
		}
	}

	public class Strategy : TradingStrategy<Settings>
	{
		private Indicator kalman;

		public override object Settings
		{
			get { return settings; }
			set
			{
				base.Settings = value;

				dynamic obj = value;
				settings.MeasurementNoise = (double?)obj.MeasurementNoise;
				settings.PlantNoise = (double?)obj.PlantNoise;
				settings.SmoothingEnabled = (bool)obj.SmoothingEnabled;
			}
		}

		protected override void AfterNewPeriod(int size)
		{
			base.AfterNewPeriod(size);

			kalman.AfterNewPeriod();
		}

		protected override void BeforeNewPeriod(int size)
		{
			base.BeforeNewPeriod(size);

			if (settings.PeriodCorrection)
			{
				kalman.BeforeNewPeriod(aggregator.Periods[size].Last());
			}
		}

		protected override void OnPeriodTrigger(int size)
		{
			kalman.Calculate(aggregator.Periods[size]);

			ExecuteDecision();
		}

		private void ExecuteDecision()
		{
			var result = kalman.Results[kalman.Results.Count - 1];

			if (result.Values.Count == 0) return;

			var periods = aggregator.Periods[settings.PeriodTicks[0]];
			var last = periods[periods.Count - 1];

			if (result.Signaled)
			{
				if (result.Direction == TrendDirection.Rising)
				{
					ExecuteOrder(TradeType.Buy);
				}
				else if (result.Direction == TrendDirection.Falling)
				{
					ExecuteOrder(TradeType.Sell);
				}
			}
		}


		private void ExecuteOrder(TradeType type)
		{
			Reverse(type);
		}

		public override void Initialize()
		{
			base.Initialize();

			var size = settings.PeriodTicks.Last();

			kalman = new Indicator(settings.Capacity)
			{
				PeriodValue = settings.PeriodValue,
				PlantNoise = settings.PlantNoise,
				MeasurementNoise = settings.MeasurementNoise,
				SmoothingEnabled = settings.SmoothingEnabled
			};

			if (!indicators.ContainsKey(settings.PeriodTicks[0]))
			{
				indicators.Add(size, new List<IIndicator>());
			}

			indicators[size].Add(kalman);
		}
	}
}
