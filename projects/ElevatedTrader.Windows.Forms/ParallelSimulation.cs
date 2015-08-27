using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader.Windows.Forms
{
	public static class ParallelSimulation
	{
		public static bool StopSimulation = false;

		public static event Action Tick;
		public static event Action SimulationComplete;

		public static async Task<IMultiSessionAnalyzer> RunSimulation(string symbol, string strategy, string dataSource, string provider, object settings, int tickCount, int iterations)
		{
			StopSimulation = false;

			var analyzer = new MultiSessionAnalyzer();

			var processes = new List<Task<ISessionAnalyzer>>(4);

			for (int index = 0; index < Math.Min(4, iterations); index++)
			{
				var task = Run(symbol, strategy, dataSource, provider, settings, tickCount);
				
				processes.Add(task);
			}

			var count = processes.Count;
			var completed = 0;

			try
			{
				while (completed < iterations)
				{
					var task = await Task.WhenAny(processes);

					DoSimulationComplete();

					var index = processes.IndexOf(task);

					analyzer.Analyze(task.Result);

					completed++;

					if (count < iterations)
					{
						processes[index] = Run(symbol, strategy, dataSource, provider, settings, tickCount);

						count++;
					}
					else
					{
						processes.Remove(task);
					}
				}				
			}
			catch (Exception ex)
			{

			}
			finally
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			return analyzer;
		}

		private static async Task<ISessionAnalyzer> Run(string symbol, string name, string dataSource, string provider, object settings, int tickCount)
		{
			return await RunStrategy(CreateStrategy(symbol, name, settings), CreateProvider(provider, CreateDataSource(symbol, dataSource)), tickCount);
		}

		private static ITradingStrategy CreateStrategy(string symbol, string name, object settings)
		{
			var strategy = TradingStrategyScripts.Create(name);

			strategy.Session.Symbol = Instrument.Get(symbol).Item;
			strategy.Settings = settings;

			return strategy;
		}

		private static ITickDataSource CreateDataSource(string symbol, string name)
		{
			return Instrument.Get(symbol).DataSources[name];
		}

		private static ITickProvider CreateProvider(string name, ITickDataSource dataSource)
		{
			var provider = TickProvider.Create(name);

			provider.DataSource = dataSource;

			return provider;
		}

		public static async Task<ISessionAnalyzer> RunStrategy(ITradingStrategy strategy, ITickProvider provider, int tickCount)
		{
			var runner = new TradingStrategyRunner();
			runner.LimitResources = true;

			runner.Tick += delegate
			{
				if (StopSimulation)
				{
					runner.Stop();
				}

				DoTick();
			};

			try
			{
				await Task.Run(() => { runner.Run(strategy, provider, tickCount); });
			}
			catch (Exception ex)
			{
				throw;
			}

			var analyzer = new SessionAnalyzer();

			analyzer.Analyze(strategy.Session);

			return analyzer;
		}

		private static readonly object lockTick = new object();

		private static void DoTick()
		{
			if (Tick != null)
			{
				lock (lockTick) { Tick(); }
			}
		}

		private static void DoSimulationComplete()
		{
			if (SimulationComplete != null)
			{
				SimulationComplete();
			}
		}
	}
}
