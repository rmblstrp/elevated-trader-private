using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class HullStrategy : TradingStrategy
{
	private HullMovingAverage hma;

	public class StrategySettings : TradingStrategySettings
	{
		private int length = 8;

		public int Length
		{
			get { return length; }
			set { length = value; }
		}

		public StrategySettings()
		{
			
		}
	}

	private StrategySettings settings = new StrategySettings();

	public override TradingStrategySettings Settings
	{
		get { return settings; }
		set { settings = (StrategySettings)value; }
	}

	public override Type SettingsType
	{
		get { return typeof(StrategySettings); }
	}

	public override void AddTick(ITradeTick tick)
	{
		base.AddTick(tick);

		hma.Calculate(aggregator.Periods[settings.PeriodTicks]);
	}

	protected override void AfterNewPeriod(int size)
	{
		base.AfterNewPeriod(size);

		ExecuteDecision();

		hma.NewPeriod();
	}

	protected override void BeforeNewPeriod(int size)
	{
		base.BeforeNewPeriod(size);
	}

	private void ExecuteDecision()
	{
		var result = hma.Results[hma.Results.Count - 1];

		if (result.Signaled)
		{
			if (result.Direction == TrendDirection.Rising)
			{
				Buy();
			}
			else if (result.Direction == TrendDirection.Falling)
			{
				Sell();
			}
		}
	}

	public override void Initialize()
	{
		base.Initialize();

		aggregator.AddSize(settings.PeriodTicks, settings.Capacity);
		hma = new HullMovingAverage(settings.Capacity)
		{
			Length = settings.Length
		};

		if (!indicators.ContainsKey(settings.PeriodTicks))
		{
			indicators.Add(settings.PeriodTicks, new List<IIndicator>());
		}

		indicators[settings.PeriodTicks].Add(hma);
	}

	private void Buy()
	{
		if (session.Position == 0)
		{
			session.Buy(aggregator);
		}
		else
		{
			session.Reverse(aggregator);
		}
	}

	private void Sell()
	{
		if (session.Position == 0)
		{
			session.Sell(aggregator);
		}
		else
		{
			session.Reverse(aggregator);
		}
	}
}
