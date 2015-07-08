using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class PeriodValueStrategy : TradingStrategy
{
	private SimplePeriodValue periodValue;
	private bool descisionExecuted = false;

	public override void AddTick(ITradeTick tick)
	{
		base.AddTick(tick);

		//var list = aggregator.Periods[settings.PeriodTicks[0]];
		//var period = list[list.Count - 1];

		//if (!descisionExecuted && period.TickCount >= settings.PeriodTicks[0] * settings.TickPercentage)
		//{
		//	periodValue.Calculate(aggregator.Periods[settings.PeriodTicks[0]]);

		//	ExecuteDecision();

		//	descisionExecuted = true;
		//}
	}

	protected override void AfterNewPeriod(int size)
	{
		base.AfterNewPeriod(size);

		//periodValue.NewPeriod();
		//descisionExecuted = false;
		//wasSignaled = false;
	}

	protected override void BeforeNewPeriod(int size)
	{
		base.BeforeNewPeriod(size);

		//periodValue.Calculate(aggregator.Periods[settings.PeriodTicks[0]]);

		//var result = periodValue.Results[periodValue.Results.Count - 1];

		//if (descisionExecuted && settings.PeriodCorrection && direction != result.Direction || !wasSignaled)
		//{
		//	ExecuteDecision();
		//}
	}

	private TrendDirection direction;
	private bool wasSignaled = false;

	private void ExecuteDecision()
	{
		//var result = periodValue.Results[periodValue.Results.Count - 1];

		//if (result.Signaled)
		//{
		//	wasSignaled = true;

		//	if (result.Direction == TrendDirection.Rising)
		//	{
		//		ExecuteOrder(TradeType.Buy);
		//	}
		//	else if (result.Direction == TrendDirection.Falling)
		//	{
		//		ExecuteOrder(TradeType.Sell);
		//	}
		//}

		//direction = result.Direction;
	}

	private void ExecuteOrder(TradeType type)
	{
		Reverse(type);
	}

	public override void Initialize()
	{
		base.Initialize();

		//aggregator.AddSize(settings.PeriodTicks[0], settings.Capacity);
		//periodValue = new SimplePeriodValue(settings.Capacity)
		//{
		//	PeriodValue = settings.PeriodValue
		//};

		//if (!indicators.ContainsKey(settings.PeriodTicks[0]))
		//{
		//	indicators.Add(settings.PeriodTicks[0], new List<IIndicator>());
		//}

		//indicators[settings.PeriodTicks[0]].Add(periodValue);
	}
}
