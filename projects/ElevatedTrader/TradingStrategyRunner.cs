using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingStrategyRunner : ITradingStrategyRunner
	{
		protected bool running = false;

		public event EventHandler<int> Tick;

		public void Run(ITradingStrategy strategy, ITradeTickProvider ticks)
		{			
			ticks.Reset();

			strategy.Initialize();

			running = true;

			int count = 1;

			while (running)
			{
				if (ticks.Next() == TickProviderResult.Ticked)
				{
					strategy.AddTick(ticks.Tick);

					DoOnTick(count++);
				}
				else
				{
					Stop();
				}
			}
		}

		public void Stop()
		{
			running = false;
		}

		protected void DoOnTick(int count)
		{
			if (Tick != null)
			{
				Tick(this, count);
			}
		}
	}
}
