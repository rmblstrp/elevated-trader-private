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

		public IList<IIndicatorResult> Results
		{
			get { return results; }
		}

		public PeriodValueType PeriodValue
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

			NewPeriod();
		}

		public void Calculate(IList<ITradingPeriod> periods)
		{
			if (periods.Count < 2) return;

			var current = periods[periods.Count - 1];
			var current_price = current.PeriodValue(PeriodValue);			
			var previous = periods[periods.Count - 2];
			var previous_price = previous.PeriodValue(PeriodValue);
			
			var distribution = MathNet.Numerics.Distributions.Normal.Estimate(current.Ticks);
			var velocity = current_price - previous_price;
			var variance = current.High - current.Low;
			//var interval = previous.TickCount;
			var interval = 1;
			var efficiency = Math.Pow(1 - previous.EfficiencyRatio, 2);

			if (periods.Count == 2)
			{
				InitializeKalman(current_price, variance, velocity, interval, efficiency);

				return;
			}


			

			var F = Matrix<double>.Build.Dense(2, 2, new[] { 1d, 0d, interval, 1 });   // State transition matrix
			var G = Matrix<double>.Build.Dense(2, 1, new[] { (interval * interval) / 2d, interval });   // Plant noise matrix
			var Q = Matrix<double>.Build.Dense(1, 1, new[] { efficiency }); // Plant noise variance

			var R = Matrix<double>.Build.Dense(1, 1, new[] { efficiency }); // Measurement variance matrix
			var H = Matrix<double>.Build.Dense(1, 2, new[] { 1d, 0d }); // Measurement matrix

			var update = Matrix<double>.Build.Dense(1, 1, new[] { current_price });

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

		private void InitializeKalman(double price, double variance, double velocity, double interval, double efficiency)
		{
			var x0 = Matrix<double>.Build.Dense(2, 1, new[] { price, velocity / interval });
			var p0 = Matrix<double>.Build.Dense(2, 2, new[] { efficiency, efficiency / interval, efficiency / interval, 2 * efficiency / (interval * interval) });

			var F = Matrix<double>.Build.Dense(2, 2, new[] { 1d, 0d, interval, 1 });   // State transition matrix
			var G = Matrix<double>.Build.Dense(2, 1, new[] { (interval * interval) / 2d, interval });   // Plant noise matrix
			var Q = Matrix<double>.Build.Dense(1, 1, new[] { efficiency }); // Plant noise variance

			kalman = new DiscreteKalmanFilter(x0, p0);
			kalman.Predict(F, G, Q);
		}

		public void NewPeriod()
		{
			Results.Add(new IndicatorResult());
		}
	}

	public class Strategy : TradingStrategy
	{
		private Indicator kalman;

		protected override void AfterNewPeriod(int size)
		{
			base.AfterNewPeriod(size);

			kalman.NewPeriod();
		}

		protected override void BeforeNewPeriod(int size)
		{
			base.BeforeNewPeriod(size);

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
				PeriodValue = settings.PeriodValue
			};

			if (!indicators.ContainsKey(settings.PeriodTicks[0]))
			{
				indicators.Add(size, new List<IIndicator>());
			}

			indicators[size].Add(kalman);
		}
	}
}
