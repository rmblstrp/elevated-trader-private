using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class HullMovingAverage : ISymbolIndicator
{
	protected List<ISymbolIndicatorResult> results;

	public bool IsStockPriceRelated
	{
		get { return true; }
	}

	public int Length
	{
		get;
		set;
	}

	public PeriodValueType PeriodValue { get; set; }

	public IList<ISymbolIndicatorResult> Results
	{
		get { return results; }
	}

	public HullMovingAverage() : this(512)
	{
	}

	public HullMovingAverage(int capacity)
	{
		results = new List<ISymbolIndicatorResult>(capacity);
		AfterNewPeriod();
	}

	public void Calculate(IList<ITradingPeriod> periods)
	{
		var required_size = (int)(Length * 2);
		
		if (periods.Count < required_size) return;

		var values = new List<double>(required_size);

		for (int index = Math.Max(0, periods.Count - required_size); index < periods.Count; index++)
		{
			values.Add(periods[index].PeriodValue(PeriodValue));
		}

		var hma = MathHelper.HullMovingAverage(values);

		var result = (IndicatorResult)Results[Results.Count - 1];
		result.Values.Clear();
		result.Values.Add(hma);

		if (Results.Count > 1)
		{
			var last = Results[Results.Count - 2];

			if (last.Values.Count > 0)
			{
				if (hma == last.Values[0])
				{
					result.Direction = TrendDirection.Sideways;
				}
				else
				{
					result.Direction = hma > last.Values[0]
						? TrendDirection.Rising
						: TrendDirection.Falling;
				}
			}

			result.Signaled = last.Direction != TrendDirection.None && last.Direction != result.Direction;
		}
	}

	public void AfterNewPeriod()
	{
		Results.Add(new IndicatorResult());
	}

	public void Clear(int keep = 0)
	{
		var list = new List<ISymbolIndicatorResult>();

		for (int index = results.Count - keep - 1; index < results.Count; index++)
		{
			list.Add(results[index]);
		}

		results.Clear();
		results = list;
	}
}
