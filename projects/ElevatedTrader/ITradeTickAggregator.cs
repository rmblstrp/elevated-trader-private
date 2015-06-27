using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeTickAggregator
	{
		IDictionary<int, int> Indicies { get; }

		IDictionary<int, ITradingPeriod> Periods { get; }

		void Add(ITradeTick tick, int size);

		event Action<int> BeforeNewPeriod;
	}
}
