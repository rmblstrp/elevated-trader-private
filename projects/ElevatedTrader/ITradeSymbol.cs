using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeSymbol
	{
		string Symbol
		{
			get;
		}

		double TickRate
		{
			get;
		}

		double TickValue
		{
			get;
		}
	}
}
