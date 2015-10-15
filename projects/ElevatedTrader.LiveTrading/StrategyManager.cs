using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElevatedTrader.DataSources;

namespace ElevatedTrader.LiveTrading
{
	public class StrategyManager : ITickReceiver
	{
		protected Thread Process
		{
			get;
			set;
		}

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

			Process = new Thread(new ThreadStart(() => StrategyRunner.Run(Strategy, TickProvider)));
			Process.Start();
			
			Running = true;
		}

		public virtual void Stop()
		{
			if (!Running) return;

			StrategyRunner.Stop();
			Process.Join();

			Running = false;
			
		}

		public virtual void TickReceived(object sender, ITick tick)
		{
			if (!Running) return;

			DataSource.TickReceived(sender, tick);
		}
	}
}
