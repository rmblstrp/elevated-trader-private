using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeHistoryQuote
	{
		public long AskSize
		{
			get;
			set;
		}

		public System.DateTime BidTime
		{
			get;
			set;
		}

		public System.DateTime AskTime
		{
			get;
			set;
		}

		public char BidExchangeCode
		{
			get;
			set;
		}

		public char AskExchangeCode
		{
			get;
			set;
		}

		public double BidPrice
		{
			get;
			set;
		}

		public long BidSize
		{
			get;
			set;
		}

		public double AskPrice
		{
			get;
			set;
		}
	}
}
