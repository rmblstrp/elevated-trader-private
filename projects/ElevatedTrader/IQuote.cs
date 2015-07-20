using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface IQuote
	{
		double Bid { get; }

		double Ask { get; }
	}
}
