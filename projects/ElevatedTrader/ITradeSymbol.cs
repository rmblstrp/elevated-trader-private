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
			set;
		}

		double TickRate
		{
			get;
			set;
		}

		double TickValue
		{
			get;
			set;
		}

		double OpenCost
		{
			get;
			set;
		}

		double PerTradeCost
		{
			get;
			set;
		}

		double PerQuanityCost
		{
			get;
			set;
		}

		int Slippage
		{
			get;
			set;
		}
	}
}
