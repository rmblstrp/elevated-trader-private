using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeOrder
	{
		IFinancialInstrument Instrument
		{
			get;
			set;
		}

		int Quantity
		{
			get;
			set;
		}

		double Bid
		{
			get;
			set;
		}

		double Ask
		{
			get;
			set;
		}

		IList<ITradeResult> Results
		{
			get;
		}	
	}
}
