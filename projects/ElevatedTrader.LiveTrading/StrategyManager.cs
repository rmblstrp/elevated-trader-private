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

		public bool Running
		{
			get;
			protected set;
		}

		public LiveTickDataSource DataSource
		{
			get;
			protected set;
		}

		public LiveTickProvider TickProvider
		{
			get;
			protected set;
		}

		public ITradingStrategy Strategy
		{
			get;
			protected set;
		}

		public TradingStrategyRunner StrategyRunner
		{
			get;
			protected set;
		}

		public TradeEventBroadcaster Broadcaster
		{
			get;
			protected set;
		}

		public event Action<Exception> ExceptionThrown;

		public StrategyManager()
		{
			Broadcaster = new TradeEventBroadcaster();
		}

		public void Initialize(Type strategy)
		{
			Strategy = (ITradingStrategy)Activator.CreateInstance(strategy);
			StrategyRunner = new TradingStrategyRunner()
			{
				LimitResources = true
			};
			DataSource = new LiveTickDataSource();
			TickProvider = new LiveTickProvider()
			{
				DataSource = DataSource
			};			
		}

		public virtual void Start()
		{
			if (Running) return;

			Broadcaster.Attach(Strategy.Session);

			Process = new Thread(new ThreadStart(RunStrategy));
			Process.Start();
			
			Running = true;
		}

		public virtual void Stop()
		{
			if (!Running) return;

			StrategyRunner.Stop();
			Process.Join();

			Broadcaster.Detach(Strategy.Session);

			Running = false;
			
		}

		public virtual void TickReceived(object sender, ITick tick)
		{
			if (!Running) return;

			DataSource.TickReceived(sender, tick);
		}

		protected void RunStrategy()
		{
			try
			{
				StrategyRunner.Run(Strategy, TickProvider);
			}
			catch (Exception ex)
			{
				Stop();

				if (ExceptionThrown != null)
				{
					ExceptionThrown(ex);
				}				
			}
		}
	}
}
