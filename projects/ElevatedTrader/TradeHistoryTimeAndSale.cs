using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ElevatedTrader
{
	public class TradeHistoryTimeAndSale
	{
		public long EventId
		{
			get;
			set;
		}

		public System.DateTime Time
		{
			get;
			set;
		}

		public char ExchangeCode
		{
			get;
			set;
		}

		public double Price
		{
			get;
			set;
		}

		public long Size
		{
			get;
			set;
		}

		public double BidPrice
		{
			get;
			set;
		}

		public double AskPrice
		{
			get;
			set;
		}

		public string ExchangeSaleConditions
		{
			get;
			set;
		}

		public bool IsTrade
		{
			get;
			set;
		}

		public int Type
		{
			get;
			set;
		}
	}
}
