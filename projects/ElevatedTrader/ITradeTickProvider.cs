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

		void Initialize();		

		TickProviderResult Next();

		void Reset();		
	}

	public interface ITradeTickProvider<T> : ITradeTickProvider
	{
		void Initialize(double price, IList<T> ticks);
	}
}
