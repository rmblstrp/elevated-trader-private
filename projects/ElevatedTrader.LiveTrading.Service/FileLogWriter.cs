using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ElevatedTrader.LiveTrading.Service
{
	public class FileLogWriter : ITradeLogWriter
	{
		public int Index
		{
			get;
			set;
		}

		public void WriteTrade(ITradeEntry trade)
		{
			var line = string.Format("P/L: {0:C}\tEquity: {1:C}\tPrice: {3:F4}\r\n", trade.Profit, trade.Equity, trade.Cost, trade.Price);

			File.AppendAllText(string.Format("strategy-{0}.log", Index), line);
		}
	}
}
