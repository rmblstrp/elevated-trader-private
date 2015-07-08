﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class HullStrategySettings : TradingStrategySettings
{
	private int length = 8;
	private PeriodValueType quoteType = PeriodValueType.WeightedAverage;

	public int Length
	{
		get { return length; }
		set { length = value; }
	}

	public PeriodValueType QuoteValue
	{
		get { return quoteType; }
		set { quoteType = value; }
	}

	public HullStrategySettings()
	{
	}
}

public class HullStrategy : TradingStrategy<HullStrategySettings>
{
	private HullMovingAverage hma;
	private HullMovingAverage quote;

	private bool descisionExecuted = false;

	public override object Settings
	{
		get { return settings; }
		set
		{
			base.Settings = value;

			dynamic obj = value;
			settings.QuoteValue = (PeriodValueType)obj.QuoteValue;
			settings.Length = (int)obj.Length;
			
		}
	}

	public override void AddTick(ITradeTick tick)
	{
		base.AddTick(tick);

		var list = aggregator.Periods[settings.PeriodTicks];
		var period = list[list.Count - 1];

		if (!descisionExecuted && period.TickCount >= settings.PeriodTicks * settings.TickPercentage)
		{
			hma.Calculate(aggregator.Periods[settings.PeriodTicks]);
			quote.Calculate(aggregator.Periods[settings.PeriodTicks]);

			ExecuteDecision();

			descisionExecuted = true;
		}
	}

	protected override void AfterNewPeriod(int size)
	{
		base.AfterNewPeriod(size);

		hma.NewPeriod();
		descisionExecuted = false;
		wasSignaled = false;
	}

	protected override void BeforeNewPeriod(int size)
	{
		base.BeforeNewPeriod(size);

		hma.Calculate(aggregator.Periods[settings.PeriodTicks]);

		var result = hma.Results[hma.Results.Count - 1];

		if (descisionExecuted && settings.PeriodCorrection && direction != result.Direction || !wasSignaled)
		{
			ExecuteDecision();
		}
	}

	private TrendDirection direction;
	private bool wasSignaled = false;

	private void ExecuteDecision()
	{
		var result = hma.Results[hma.Results.Count - 1];
		var periods = aggregator.Periods[settings.PeriodTicks];
		var last = periods[periods.Count - 1];

		//wasSignaled = true;

		//if (result.Values.Count == 0) return;

		//if (last.PeriodValue(settings.PeriodValue) > result.Values[0] && result.Direction == TrendDirection.Falling)
		//{
		//	ExecuteOrder(TradeType.Buy);
		//}
		//else if (last.PeriodValue(settings.PeriodValue) < result.Values[0] && result.Direction == TrendDirection.Rising)
		//{
		//	ExecuteOrder(TradeType.Sell);
		//}
		

		if (result.Signaled)
		{
			wasSignaled = true;

			if (result.Direction == TrendDirection.Rising && last.QuoteValue(settings.QuoteValue) > result.Values[0])
			{
				ExecuteOrder(TradeType.Buy);
			}
			else if (result.Direction == TrendDirection.Falling && last.QuoteValue(settings.QuoteValue) < result.Values[0])
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
		hma = new HullMovingAverage(settings.Capacity)
		{
			Length = settings.Length,
			PeriodValue = settings.PeriodValue
		};

		quote = new HullMovingAverage(settings.Capacity)
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
