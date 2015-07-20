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

		public void Run(ITradingStrategy strategy, ITickProvider ticks, int? iterations = null)
		{			
			ticks.Initialize();
			strategy.Initialize();

			running = true;

			int count = 0;

			while (running)
			{
				var result = ticks.Next();

				switch (result)
				{
					case TickProviderResult.Ticked:
						strategy.AddTick(ticks.Tick);
						DoOnTick(count + 1);
						break;
					case TickProviderResult.Done:
						Stop();
						break;
				}

				if (running && iterations.HasValue && count >= iterations)
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
