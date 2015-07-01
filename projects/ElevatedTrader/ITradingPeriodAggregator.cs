using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingPeriodAggregator
	{
		ITradeTick LastTick { get; }

		IDictionary<int, IList<ITradingPeriod>> Periods { get; }

		event Action<int> BeforeNewPeriod;

		void AddSize(int size, int capacity);

		void AddTick(ITradeTick tick);

		ITradingPeriod CurrentPeriod(int size);

		IDictionary<int, int> Indexes();

		void Reset();
	}
}
