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

	public HullMovingAverage()
	{
		results = new List<IIndicatorResult>();
	}

	public HullMovingAverage(int capacity)
	{
		results = new List<IIndicatorResult>(capacity);
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
	}

	public void NewPeriod()
	{
		throw new NotImplementedException();
	}
}
