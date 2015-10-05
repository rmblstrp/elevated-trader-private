using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class SimpleTradeExecutor : ITradeExecutor
	{
		protected readonly Random random = new Random();

		public event EventHandler<ITradeOrder> TradeExecuted;

		public event EventHandler<ITradeOrder> TradeCancelled;

		public event EventHandler<ITradeOrder> TradeFailed;

		public TradeExecutionState State
		{
			get;
			protected set;
		}

		public bool ExecuteTrade(ITradeOrder order)
		{
			if (order.Quantity == 0) return false;

			var fees = Math.Abs(order.Quantity * order.Instrument.PerQuantityCost) - order.Instrument.PerTradeCost;
			var slippage = order.Instrument.TickValue * random.Next(0, order.Instrument.Slippage);
			var result = new TradeResult()
			{
				Executed = DateTime.UtcNow,
				Price = order.Quantity > 0 ? order.Ask + slippage : order.Bid - slippage,
				Quantity = order.Quantity,
				Fees = fees
				
			};

			order.Results.Add(result);

			DoTradeExecuted(order);

			return true;
		}

		public void CancelTrade()
		{
			
		}

		protected void DoTradeExecuted(ITradeOrder order)
		{
			if (TradeExecuted != null)
			{
				TradeExecuted(this, order);
			}
		}
	}
}
