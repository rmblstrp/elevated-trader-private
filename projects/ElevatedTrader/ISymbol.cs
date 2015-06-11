using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ISymbol
	{
		string Symbol
		{
			get;
			private set;
		}

		double TickRate
		{
			get;
			private set;
		}

		double TickValue
		{
			get;
			private set;
		}
	}
}
