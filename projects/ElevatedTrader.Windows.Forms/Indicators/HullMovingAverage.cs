using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader;

public class HullMovingAverage : IIndicator
{
	protected List<IIndicatorResult> results = new List<IIndicatorResult>();

	public IList<IIndicatorResult> Results
	{
		get { return results; }
	}

	public void Calculate(IList<ITradingPeriod> periods)
	{
		throw new NotImplementedException();
	}

	public void NewPeriod()
	{
		throw new NotImplementedException();
	}
}
