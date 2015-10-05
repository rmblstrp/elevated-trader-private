using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeOrder : ITradeOrder
	{
		private readonly List<ITradeResult> results = new List<ITradeResult>();

		public IFinancialInstrument Instrument
		{
			get;
			set;
		}

		public int Quantity
		{
			get;
			set;
		}

		public double Bid
		{
			get;
			set;
		}

		public double Ask
		{
			get;
			set;
		}

		public IList<ITradeResult> Results
		{
			get { return results; }
		}
	}
}
