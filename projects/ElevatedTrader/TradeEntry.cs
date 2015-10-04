using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public struct TradeEntry : ITradeEntry
	{
		private readonly TradeType type;
		private readonly int quantity;
		private readonly double price;
		private readonly double equity;
		private readonly double profit;
		private readonly double cost;
		private readonly IDictionary<int, int> indexes;

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

		public double Cost
		{
			get { return cost; }
		}

		public IDictionary<int, int> Indexes
		{
			get { return indexes; }
		}

		public TradeEntry(TradeType type, int quantity, double price, double equity, double profit, double cost, IDictionary<int, int> indexes)
		{
			this.type = type;
			this.quantity = quantity;
			this.price = price;
			this.equity = equity;
			this.profit = profit;
			this.cost = cost;
			this.indexes = indexes;
		}
	}
}
