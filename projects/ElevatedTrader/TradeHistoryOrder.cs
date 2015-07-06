using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeHistoryOrder
	{
		public enum OrderSide
		{
			Sell = 0,
			Buy = 1,
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

		public OrderSide Side
		{
			get;
			set;
		}

		public int Level
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

		public long Index
		{
			get;
			set;
		}

		public string MarketMaker
		{
			get;
			set;
		}
	}
}
