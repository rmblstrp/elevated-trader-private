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

		public virtual IDictionary<int, int> Indexes
		{
			get;
			protected set;
		}

		public Trade(TradeType type, int quantity, double price, IDictionary<int, int> indexes)
		{
			Type = type;
			Quantity = quantity;
			Price = price;
			Indexes = indexes;
		}
	}
}
