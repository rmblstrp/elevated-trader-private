using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class Trade : ITrade
	{
		public virtual TradeType Type
		{
			get;
			protected set;
		}

		public virtual int Quantity
		{
			get;
			protected set;
		}

		public virtual  double Price
		{
			get;
			protected set;
		}

		public virtual double Equity
		{
			get;
			protected set;
		}

		public virtual double Profit
		{
			get;
			protected set;
		}

		public virtual IDictionary<int, int> Indexes
		{
			get;
			protected set;
		}

		public Trade(TradeType type, int quantity, double price, double equity, double profit, IDictionary<int, int> indexes)
		{
			Type = type;
			Quantity = quantity;
			Price = price;
			Equity = equity;
			Profit = profit;
			Indexes = indexes;
		}
	}
}
