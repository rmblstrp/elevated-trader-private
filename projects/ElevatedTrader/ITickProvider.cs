using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITickProvider
	{
		ITradeSymbol Symbol
		{
			get;
		}

		ITick Tick
		{
			get;
		}

		void Initialize();		

		TickProviderResult Next();

		void Reset();		
	}

	public interface ITickProvider<T> : ITickProvider
	{
		void Initialize(double price, IList<T> ticks);
	}
}
