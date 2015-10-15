using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatedTrader.DataSources;

namespace ElevatedTrader.LiveTrading
{
	public class StrategyManager : ITickReceiver
	{
		public virtual bool Running
		{
			get;
			protected set;
		}

		public virtual LiveTickDataSource DataSource
		{
			get;
			protected set;
		}

		public virtual LiveTickProvider TickProvider
		{
			get;
			protected set;
		}

		public virtual ITradingStrategy Strategy
		{
			get;
			protected set;
		}

		public virtual TradingStrategyRunner StrategyRunner
		{
			get;
			protected set;
		}

		public void Initialize(Type strategy)
		{
			Strategy = (ITradingStrategy)Activator.CreateInstance(strategy);
			StrategyRunner = new TradingStrategyRunner();
			DataSource = new LiveTickDataSource();
			TickProvider = new LiveTickProvider()
			{
				DataSource = DataSource
			};
		}

		public virtual void Start()
		{
			if (Running) return;

			StrategyRunner.Run(Strategy, TickProvider);
			Running = true;
		}

		public virtual void Stop()
		{
			if (!Running) return;

			StrategyRunner.Stop();
			Running = false;
			
		}

		public virtual void TickReceived(object sender, ITick tick)
		{
			if (!Running) return;

			DataSource.TickReceived(sender, tick);
		}
	}
}
