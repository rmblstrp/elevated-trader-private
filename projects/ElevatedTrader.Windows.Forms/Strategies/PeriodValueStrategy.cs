using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class PeriodValueStrategy : TradingStrategy
{
	private SimplePeriodValue periodValue;

	public class StrategySettings : TradingStrategySettings
	{
		private double tickPercentage = 0.75;
		private bool periodCorrection = false;

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

	public override object Settings
	{
		get { return settings; }
		set
		{
			dynamic obj = value;

			settings.Capacity = (int)obj.Capacity;
			settings.PeriodTicks = (int)obj.PeriodTicks;
			settings.PeriodValue = (PeriodValueType)obj.PeriodValue;
			settings.ReversePositions = obj.ReversePositions;
			settings.PeriodCorrection = obj.PeriodCorrection;
			settings.TickPercentage = obj.TickPercentage;
		}
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
			periodValue.Calculate(aggregator.Periods[settings.PeriodTicks]);

			ExecuteDecision();

			descisionExecuted = true;
		}
	}

	protected override void AfterNewPeriod(int size)
	{
		base.AfterNewPeriod(size);

		periodValue.NewPeriod();
		descisionExecuted = false;
		wasSignaled = false;
	}

	protected override void BeforeNewPeriod(int size)
	{
		base.BeforeNewPeriod(size);

		periodValue.Calculate(aggregator.Periods[settings.PeriodTicks]);

		var result = periodValue.Results[periodValue.Results.Count - 1];

		if (descisionExecuted && settings.PeriodCorrection && direction != result.Direction || !wasSignaled)
		{
			ExecuteDecision();
		}
	}

	private TrendDirection direction;
	private bool wasSignaled = false;

	private void ExecuteDecision()
	{
		var result = periodValue.Results[periodValue.Results.Count - 1];

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
		if ((type == TradeType.Buy && !settings.ReversePositions) || (type == TradeType.Sell && settings.ReversePositions))
		{
			Buy();
		}
		else
		{
			Sell();
		}
	}

	public override void Initialize()
	{
		base.Initialize();

		aggregator.AddSize(settings.PeriodTicks, settings.Capacity);
		periodValue = new SimplePeriodValue(settings.Capacity)
		{
			PeriodValue = settings.PeriodValue
		};

		if (!indicators.ContainsKey(settings.PeriodTicks))
		{
			indicators.Add(settings.PeriodTicks, new List<IIndicator>());
		}

		indicators[settings.PeriodTicks].Add(periodValue);
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
