using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class SimplePeriodValue : IIndicator
{
	protected List<IIndicatorResult> results;

	public bool IsStockPriceRelated
	{
		get { return true; }
	}

	public PeriodValueType PeriodValue { get; set; }

	public IList<IIndicatorResult> Results
	{
		get { return results; }
	}

	public SimplePeriodValue() : this(512)
	{
	}

	public SimplePeriodValue(int capacity)
	{
		results = new List<IIndicatorResult>(capacity);
		NewPeriod();
	}

	public void Calculate(IList<ITradingPeriod> periods)
	{
		var current = periods.Last();
		var pv = current.Value(PeriodValue);
		
		var result = (IndicatorResult)Results[Results.Count - 1];
		result.Values.Clear();
		result.Values.Add(pv);

		if (Results.Count > 1)
		{
			var previous = Results[Results.Count - 2];

			if (previous.Values.Count > 0)
			{
				if (pv == previous.Values[0])
				{
					result.Direction = TrendDirection.Sideways;
				}
				else
				{
					result.Direction = pv > previous.Values[0]
						? TrendDirection.Rising
						: TrendDirection.Falling;
				}
			}

			result.Signaled = previous.Direction != TrendDirection.None && previous.Direction != result.Direction;
		}
	}

	public void NewPeriod()
	{
		Results.Add(new IndicatorResult());
	}
}
