using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader.Indicators
{
	public interface IIndicatorResult
	{
		bool SignalIndicated
		{
			get;
			private set;
		}

		TrendDirection Direction
		{
			get;
			private set;
		}
	}
}
