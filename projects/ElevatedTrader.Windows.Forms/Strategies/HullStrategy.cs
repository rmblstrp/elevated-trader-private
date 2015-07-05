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
		private double tickPercentage = 0.75;
		private bool periodCorrection = false;

		public int Length
		{
			get { return length; }
			set { length = value; }
		}

		public bool PeriodCorrection
		{
			get { return periodCorrection; }
			set { periodCorrection = value; }
		}

		public double TickPercentage
		{
			get { return tickPercentage; }
			set { tickPercentage = value; }
		}

		public StrategySettings()
		{
			
		}
	}

	private StrategySettings settings = new StrategySettings();
	private bool descisionExecuted = false;

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

		var list = aggregator.Periods[settings.PeriodTicks];
		var period = list[list.Count - 1];

		if (!descisionExecuted && period.TickCount >= settings.PeriodTicks * settings.TickPercentage)
		{
			hma.Calculate(aggregator.Periods[settings.PeriodTicks]);

			ExecuteDecision();

			descisionExecuted = true;
		}
	}

	protected override void AfterNewPeriod(int size)
	{
		base.AfterNewPeriod(size);

		if (descisionExecuted && settings.PeriodCorrection)
		{
			hma.Calculate(aggregator.Periods[settings.PeriodTicks]);

			ExecuteDecision();
		}

		hma.NewPeriod();
		descisionExecuted = false;
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
			Length = settings.Length,
			PeriodValue = settings.PeriodValue
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
