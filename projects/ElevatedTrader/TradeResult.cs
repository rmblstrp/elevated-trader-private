using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeResult : ITradeResult
	{
		public DateTime Executed
		{
			get;
			set;
		}

		public int Quantity
		{
			get;
			set;
		}

		public double Price
		{
			get;
			set;
		}

		public double Fees
		{
			get;
			set;
		}
	}
}
