using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeTickProvider
	{
		ITradeSymbol Symbol
		{
			get;
		}

		ITradeTick Tick
		{
			get;
		}

		void Initialize(ITradeSymbol symbol, IList<ITradeTick> ticks = null);

		TickProviderResult Next();

		void Reset();		
	}
}
