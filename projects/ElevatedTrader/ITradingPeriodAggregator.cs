using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingPeriodAggregator
	{
		ITick Last { get; }

		IDictionary<int, IList<ITradingPeriod>> Periods { get; }

		event Action<int> AfterNewPeriod;

		event Action<int> BeforeNewPeriod;

		void AddSize(int size, int capacity);

		void AddQuote(IQuote quote);

		void AddTick(ITick tick);

		ITradingPeriod CurrentPeriod(int size);

		IDictionary<int, int> Indexes();

		void Clear();
	}
}
