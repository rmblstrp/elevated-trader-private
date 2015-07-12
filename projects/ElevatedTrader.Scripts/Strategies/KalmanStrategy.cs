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
	public class Settings : TradingStrategySettings
	{
		private double? plantNoise = 0.1;
		private double? measurementNoise = null;
		private double transitionValue = 1;
		private double timeInterval = 1;
		private double measurementNoiseMultiplier = 1;
		private double plantNoiseMultiplier = 1;

		public double TransitionValue
		{
			get { return transitionValue; }
			set { transitionValue = value; }
		}

		public double? PlantNoise
		{
			get { return plantNoise; }
			set { plantNoise = value; }
		}

		public double PlantNoiseMultiplier
		{
			get { return plantNoiseMultiplier; }
			set { plantNoiseMultiplier = value; }
		}

		public double MeasurementNoiseMultiplier
		{
			get { return measurementNoiseMultiplier; }
			set { measurementNoiseMultiplier = value; }
		}

		public double? MeasurementNoise
		{
			get { return measurementNoise; }
			set { measurementNoise = value; }
		}

		public double TimeInterval
		{
			get { return timeInterval; }
			set { timeInterval = value; }
		}

		public bool OrderCorrection
		{
			get;
			set;
		}
	}

	public class Indicator : IIndicator<Settings>
	{
		private List<IIndicatorResult> results;
		private DiscreteKalmanFilter kalman;
		private Matrix<double> F, G, Q, H, R;

		public IList<IIndicatorResult> Results
		{
			get { return results; }
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

		public ITradeSymbol Symbol
		{
			get;
			set;
		}

		public Settings Settings
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

				var x0 = Matrix<double>.Build.Dense(2, 1, new[] { current_price, velocity / Settings.TimeInterval });
				var p0 = Matrix<double>.Build.Dense(2, 2, new[] { measurement_noise, measurement_noise / Settings.TimeInterval, measurement_noise / Settings.TimeInterval, 2 * measurement_noise / (Settings.TimeInterval * Settings.TimeInterval) });

				F = Matrix<double>.Build.Dense(2, 2, new[] { Settings.TransitionValue, 0d, Settings.TimeInterval, Settings.TransitionValue });   // State transition matrix
				G = Matrix<double>.Build.Dense(2, 1, new[] { (Settings.TimeInterval * Settings.TimeInterval) / 2d, Settings.TimeInterval });   // Plant noise matrix
				Q = Matrix<double>.Build.Dense(1, 1, new[] { plant_noise }); // Plant noise variance

				H = Matrix<double>.Build.Dense(1, 2, new[] { 1d, 0d }); // Measurement matrix
				R = Matrix<double>.Build.Dense(1, 1, new[] { measurement_noise }); // Measurement variance matrix

				kalman = new DiscreteKalmanFilter(x0, p0);
				kalman.Predict(F, G, Q);

				return;
			}

			PerformKalman(current);

			UpdateResult();
		}

		private void UpdateResult()
		{
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

				if (result.Signaled)
				{

				}
			}
		}

		private double GetMeasurementNoise(ITradingPeriod current)
		{
			// the farther away from 1 the the worse the reading
			var noise = (current.High - current.Low) / Symbol.TickRate * Settings.MeasurementNoiseMultiplier;

			if (double.IsInfinity(noise) || double.IsNaN(noise))
			{
				noise = 1;
			}

			return Settings.MeasurementNoise.HasValue ? Settings.MeasurementNoise.Value : noise;
		}

		private double GetPlantNoise(ITradingPeriod current)
		{
			// the closer the value gets to 1 the better the reading
			var noise = 1 / ((current.High - current.Low) / Symbol.TickRate) * Settings.PlantNoiseMultiplier;

			if (double.IsInfinity(noise) || double.IsNaN(noise))
			{
				noise = 1;
			}

			return Settings.PlantNoise.HasValue ? Settings.PlantNoise.Value : Math.Max(noise, Math.Min(noise, 0));
		}

		private double GetPeriodPrice(ITradingPeriod current)
		{
			return current.PeriodValue(Settings.PeriodValue);
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

		public void BeforeNewPeriod(ITradingPeriod current, bool updateResult)
		{
			if (kalman != null)
			{
				PerformKalman(current);

				if (updateResult)
				{
					UpdateResult();
				}
			}
		}

		public void AfterNewPeriod()
		{
			Results.Add(new IndicatorResult());
		}
	}

	public class Strategy : TradingStrategy<Settings>
	{
		private Indicator kalman;
		private TrendDirection direction;

		public override object Settings
		{
			get { return settings; }
			set
			{
				base.Settings = value;

				dynamic obj = value;
				settings.MeasurementNoise = (double?)obj.MeasurementNoise;
				settings.PlantNoise = (double?)obj.PlantNoise;
				settings.TransitionValue = (double)obj.TransitionValue;
				settings.TimeInterval = (double)obj.TimeInterval;
				settings.MeasurementNoiseMultiplier = (double)obj.MeasurementNoiseMultiplier;
				settings.PlantNoiseMultiplier = (double)obj.PlantNoiseMultiplier;
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
				kalman.BeforeNewPeriod(aggregator.Periods[size].Last(), settings.OrderCorrection);

				if (settings.OrderCorrection)
				{
					ExecuteDecision(true);
				}
			}
		}

		protected override void OnPeriodTrigger(int size)
		{
			kalman.Calculate(aggregator.Periods[size]);

			ExecuteDecision();
		}

		private void ExecuteDecision(bool correction = false)
		{
			var result = kalman.Results[kalman.Results.Count - 1];

			if (result.Values.Count == 0) return;

			var periods = aggregator.Periods[settings.PeriodTicks[0]];
			var last = periods[periods.Count - 1];

			if (result.Signaled || (correction && result.Direction != direction && direction != TrendDirection.None))
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

			direction = result.Direction;
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
				Settings = settings,
				Symbol = session.Symbol
			};

			if (!indicators.ContainsKey(settings.PeriodTicks[0]))
			{
				indicators.Add(size, new List<IIndicator>());
			}

			indicators[size].Add(kalman as IIndicator);
		}
	}
}