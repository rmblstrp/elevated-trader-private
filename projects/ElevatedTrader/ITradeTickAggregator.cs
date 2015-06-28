using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeTickAggregator
	{
		IDictionary<int, int> Indexes { get; }

		ITradeTick Last { get; }

		IDictionary<int, ITradingPeriod> Periods { get; }

		event Action<int> BeforeNewPeriod;

		void Add(ITradeTick tick, int size);

		void Reset();		
	}
}
