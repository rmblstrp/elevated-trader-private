using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

namespace Hull
{
	public class Settings : TradingStrategySettings
	{
		private int length = 8;

		public int Length
		{
			get { return length; }
			set { length = value; }
		}
	}

	public class Strategy : TradingStrategy<Settings>
	{
		private HullMovingAverage hma;
		private TrendDirection direction;
		private bool wasSignaled = false;

		public override object Settings
		{
			get { return settings; }
			set
			{
				base.Settings = value;

				dynamic obj = value;
				settings.Length = (int)obj.Length;

			}
		}

		protected override void AfterNewPeriod(int size)
		{
			base.AfterNewPeriod(size);

			hma.AfterNewPeriod();

			wasSignaled = false;
		}

		protected override void BeforeNewPeriod(int size)
		{
			base.BeforeNewPeriod(size);

			if (wasSignaled && settings.PeriodCorrection)
			{
				hma.Calculate(aggregator.Periods[size]);

				var result = hma.Results[hma.Results.Count - 1];
				var periods = aggregator.Periods[settings.PeriodTicks[0]];
				var last = periods[periods.Count - 1];

				if (result.Direction != direction)
				{
					if (direction == TrendDirection.Rising)
					{
						ExecuteOrder(TradeType.Sell);
					}
					else
					{
						ExecuteOrder(TradeType.Buy);
					}
				}
			}
		}

		protected override void OnPeriodTrigger(int size)
		{
			hma.Calculate(aggregator.Periods[size]);

			ExecuteDecision();
		}

		private void ExecuteDecision()
		{
			var result = hma.Results[hma.Results.Count - 1];
			var periods = aggregator.Periods[settings.PeriodTicks[0]];
			var last = periods[periods.Count - 1];

			if (result.Values.Count == 0) return;

			if (result.Signaled)
			{
				wasSignaled = true;

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
		
			hma = new HullMovingAverage(settings.Capacity)
			{
				Length = settings.Length,
				PeriodValue = settings.PeriodValue
			};

			if (!indicators.ContainsKey(settings.PeriodTicks[0]))
			{
				indicators.Add(size, new List<IIndicator>());
			}

			indicators[size].Add(hma);
		}
	}
}