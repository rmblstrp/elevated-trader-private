using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeHistoryTrade
	{
		public double DayVolume
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
	}
}
