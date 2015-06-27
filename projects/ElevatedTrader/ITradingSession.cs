using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingSession
	{
		ITradeSymbol Symbol { get; set; }

		IEnumerable<ITrade> Trades { get; set; }

		void ExecuteBuy();
		void ExecuteSell();
		void ExecuteReversal();
	}
}
