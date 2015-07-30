using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingSession
	{
		double Equity { get; }

		IInstrument Symbol { get; set; }

		int Position { get; }

		IList<ITrade> Trades { get; }

		event EventHandler<ITrade> Trade;

		void Buy(ITradingPeriodAggregator aggregator, int quantity = 1);
		void Sell(ITradingPeriodAggregator aggregator, int quantity = 1);
		void Reverse(ITradingPeriodAggregator aggregator);

		void Reset();
	}
}
