using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeTickAggregator
	{
		Dictionary<int, ITradePeriod> Periods { get; }

		void Add(ITradeTick tick, int size);

		event Action<int> BeforeNewPeriod;
	}
}
