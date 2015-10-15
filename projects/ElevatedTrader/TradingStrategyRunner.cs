using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingStrategyRunner : ITradingStrategyRunner
	{
		protected bool Running
		{
			get;
			set;
		}

		public bool LimitResources { get; set; }

		public event EventHandler<int> Tick;

		public void Run(ITradingStrategy strategy, ITickProvider ticks, int? iterations = null)
		{
			ticks.Initialize();
			strategy.Initialize();

			Running = true;

			int count = 0;

			while (Running)
			{
				var result = ticks.Next();

				switch (result)
				{
					case TickProviderResult.Ticked:
						strategy.AddTick(ticks.Tick);
						DoOnTick(++count);
						break;
					case TickProviderResult.Done:
						Stop();
						break;
					case TickProviderResult.None:
						Thread.Sleep(5);
						break;
				}

				if (LimitResources && count % 5000000 == 0)
				{
					strategy.FreeResources();
				}

				if (Running && iterations.HasValue && count >= iterations)
				{
					Stop();
				}
			}

			strategy.End();
		}

		public void Stop()
		{
			Running = false;
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
