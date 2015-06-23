using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingSession
	{
		TradePosition Position { get; set; }

		ITradeSymbol Symbol { get; set; }

		IEnumerable<ITrade> Trades { get; set; }		

		void ExecuteBuy(int quantity = 1);
		void ExecuteSell(int quantity = 1);
		void ReversePosition();

		void Tick(ITradeTick tick);
	}
}
