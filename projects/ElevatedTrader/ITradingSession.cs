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

		void ExecuteBuy(IDictionary<int, int> indicies);
		void ExecuteSell(IDictionary<int, int> indicies);
		void ExecuteReversal(IDictionary<int, int> indicies);

		void Initialize(int quantity = 0, int sizing = 1);
	}
}
