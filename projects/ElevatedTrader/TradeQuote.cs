using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeQuote : ITradeQuote
	{
		public virtual double Bid
		{
			get;
			set;
		}

		public virtual double Ask
		{
			get;
			set;
		}
	}
}
