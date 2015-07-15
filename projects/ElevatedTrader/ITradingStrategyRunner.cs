using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingStrategyRunner
	{
		event EventHandler<int> Tick;

		void Run(ITradingStrategy strategy, ITradeTickProvider ticks);

		void Stop();
	}
}
