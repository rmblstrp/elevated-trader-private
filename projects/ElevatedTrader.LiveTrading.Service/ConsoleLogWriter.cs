using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ElevatedTrader.LiveTrading.Service
{
	public class ConsoleLogWriter : ITradeLogWriter
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public int Index
		{
			get;
			set;
		}

		public void WriteTrade(ITradeEntry trade)
		{
			logger.Info(string.Format("Trade[{4}] - P/L: {0:C}\tEquity: {1:C}\tPrice: {3:F4}", trade.Profit, trade.Equity, trade.Cost, trade.Price, Index));
		}
	}
}
