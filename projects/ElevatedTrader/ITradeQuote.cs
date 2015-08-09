using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeQuote
	{
		double Bid { get; }

		double Ask { get; }
	}
}
