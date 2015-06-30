using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class HullMovingAverage : IIndicator
{
	protected List<IIndicatorResult> results;

	public int Length
	{
		get;
		set;
	}

	public PriceType PriceValue
	{
		get;
		set;
	}

	public IList<IIndicatorResult> Results
	{
		get { return results; }
	}

	public HullMovingAverage() : this(512)
	{
	}

	public HullMovingAverage(int capacity)
	{
		results = new List<IIndicatorResult>(capacity);
		NewPeriod();
	}

	public void Calculate(IList<ITradingPeriod> periods)
	{
		var required_size = Length * 2;

		if (periods.Count < required_size) return;

		var values = new List<double>(required_size);

		for (int index = periods.Count - required_size - 1; index < periods.Count; index++)
		{
			values.Add(periods[index].Value(PriceValue));
		}

		var hma = MathHelper.HullMovingAverage(values);

		var result = Results[Results.Count - 1];
		result.Values.Clear();
		result.Values.Add(hma);

		if (Results.Count > 1)
		{
			var last = Results[Results.Count - 2];

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

			result.Signaled = last.Direction != TrendDirection.None && last.Direction != result.Direction;
		}
	}

	public void NewPeriod()
	{
		Results.Add(new IndicatorResult());
	}
}
