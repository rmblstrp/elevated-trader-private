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

		public double PlantNoise
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

		private int smoothingValue = 1;

		public int SmoothingValue
		{
			get { return smoothingValue; }
			set { smoothingValue = (int)Math.Max(1, value); }
		}

		public void Calculate(IList<ITradingPeriod> periods)
		{
			if (periods.Count < 2) return;

			var current = periods[periods.Count - 1];
			var current_price = current.PeriodValue(PeriodValue);
			var previous = periods[periods.Count - 2];
			var previous_price = previous.PeriodValue(PeriodValue);
			var distribution = MathNet.Numerics.Distributions.Normal.Estimate(current.Ticks);
			//var measurement_noise = Math.Sqrt(distribution.StdDev);
			var measurement_noise = MeasurementNoise.HasValue ? MeasurementNoise.Value : distribution.StdDev;
			var plant_noise = PlantNoise;

			if (kalman == null)
			{
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

			if (SmoothingEnabled)
			{
				for (int index = 0; index < current.Ticks.Count - 1; index++)
				{
					if (index % SmoothingValue == 0)
					{
						var tick_price = current.Ticks[index];
						var smoothing = Matrix<double>.Build.Dense(1, 1, new[] { tick_price });

						kalman.Update(smoothing, H, R);
						kalman.Predict(F, G, Q);
					}
				}
			}

			var update = Matrix<double>.Build.Dense(1, 1, new[] { current_price });
			R[0, 0] = measurement_noise;

			kalman.Update(update, H, R);
			kalman.Predict(F, G, Q);

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

		public void BeforeNewPeriod(ITradingPeriod current)
		{
			if ( kalman != null)
			{
				var current_price = current.PeriodValue(PeriodValue);				
				var distribution = MathNet.Numerics.Distributions.Normal.Estimate(current.Ticks);
				var measurement_noise = MeasurementNoise.HasValue ? MeasurementNoise.Value : distribution.StdDev;

				var update = Matrix<double>.Build.Dense(1, 1, new[] { current_price });
				R[0, 0] = measurement_noise;
				
				kalman.Update(update, H, R);
				kalman.Predict(F, G, Q);
			}
		}

		public void AfterNewPeriod()
		{
			Results.Add(new IndicatorResult());
		}
	}

	public class Settings : TradingStrategySettings
	{
		private double plantNoise = 0.1;
		private double? measurementNoise = null;
		private bool smoothingEnabled = false;
		private int smoothingValue = 1;

		public bool SmoothingEnabled
		{
			get { return smoothingEnabled; }
			set { smoothingEnabled = value; }
		}

		public int SmoothingValue
		{
			get { return smoothingValue; }
			set { smoothingValue = value; }
		}

		public double PlantNoise
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
				settings.PlantNoise = (double)obj.PlantNoise;
				settings.SmoothingEnabled = (bool)obj.SmoothingEnabled;
				settings.SmoothingValue = (int)obj.SmoothingValue;
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
				SmoothingEnabled = settings.SmoothingEnabled,
				SmoothingValue = settings.SmoothingValue
			};

			if (!indicators.ContainsKey(settings.PeriodTicks[0]))
			{
				indicators.Add(size, new List<IIndicator>());
			}

			indicators[size].Add(kalman);
		}
	}
}
