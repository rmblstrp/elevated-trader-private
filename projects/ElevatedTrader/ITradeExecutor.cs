using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeExecutor
	{
		event EventHandler<ITradeOrder> TradeExecuted;
		event EventHandler<ITradeOrder> TradeCancelled;
		event EventHandler<ITradeOrder> TradeFailed;

		TradeExecutionState State { get; }

		bool ExecuteTrade(ITradeOrder order);
		void CancelTrade();
	}
}
