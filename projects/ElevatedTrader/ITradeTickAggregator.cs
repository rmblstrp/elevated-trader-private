using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeTickAggregator
	{
		ITradeTick Last { get; }

		IDictionary<int, IList<ITradingPeriod>> Periods { get; }

		event Action<int> BeforeNewPeriod;

		void AddSize(int size);

		void AddTick(ITradeTick tick);

		IDictionary<int, int> Indexes();

		void Reset();		
	}
}
