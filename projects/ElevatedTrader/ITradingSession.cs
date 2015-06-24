using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingSession
	{
		int AggregateTickCount { get; set; }

		int InitialQuantity { get; set; }

		ITradingStrategy Strategy { get; set; }

		ITradeSymbol Symbol { get; set; }

		int TradeQuantity { get; set; }

		IEnumerable<ITrade> Trades { get; set; }

		void Tick(ITradeTick tick);
	}
}
