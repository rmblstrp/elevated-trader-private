using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public struct Trade : ITrade
	{
		private TradeType type;
		private int quantity;
		private double price;
		private double equity;
		private double profit;
		private IDictionary<int, int> indexes;

		public TradeType Type
		{
			get { return type; }
		}

		public int Quantity
		{
			get { return quantity; }
		}

		public  double Price
		{
			get { return price; }
		}

		public double Equity
		{
			get { return equity; }
		}

		public double Profit
		{
			get { return profit; }
		}

		public IDictionary<int, int> Indexes
		{
			get { return indexes; }
		}

		public Trade(TradeType type, int quantity, double price, double equity, double profit, IDictionary<int, int> indexes)
		{
			this.type = type;
			this.quantity = quantity;
			this.price = price;
			this.equity = equity;
			this.profit = profit;
			this.indexes = indexes;
		}
	}
}
