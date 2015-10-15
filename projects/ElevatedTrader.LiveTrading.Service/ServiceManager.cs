using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace ElevatedTrader.LiveTrading.Service
{
	public class ServiceManager
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();
		private const string InstrumentsFilename = "instruments.json";
		private const string SettingsFilename = "settings.json";

		private readonly Dictionary<string, TradeInstrument> instruments = new Dictionary<string, TradeInstrument>();
		private readonly List<StrategyManager> managerList = new List<StrategyManager>();
		private readonly Dictionary<int, ITradeEventReceiver> loggerList = new Dictionary<int, ITradeEventReceiver>();
		private Settings settings;
		private ITradeDataLink dataLink;

		public void Start()
		{			
			Initialize();
			RunStrategies();
		}

		public void Stop()
		{
			StopStrategies();
		}

		private void Initialize()
		{
			LoadInstruments();
			LoadSettings();
			LoadDataLink();
			LoadStrategies();
			AttachReceivers();
		}

		private void RunStrategies()
		{
			for (int index = 0; index < managerList.Count; index++)
			{
				logger.Info("Starting strategies");
				managerList[index].Start();
			}

			AttachReceivers();
			AttachLoggers();

			logger.Info("Connecting data link");
			dataLink.Connect();
		}

		private void StopStrategies()
		{
			logger.Info("Disconnecting data link");
			dataLink.Disconnect();

			DetachReceivers();
			DetachLoggers();

			logger.Info("Stopping strategies");
			for (int index = 0; index < managerList.Count; index++)
			{
				managerList[index].Stop();
			}
		}

		private void LoadDataLink()
		{
			logger.Info("Loading data link");
			var type = Type.GetType(settings.DataLink.Type);
			dataLink = (ITradeDataLink)Activator.CreateInstance(type);

			if (dataLink as IConfigurable != null)
			{
				logger.Info("Configuring data link");
				((IConfigurable)dataLink).Configure(settings.DataLink.Settings);
			}
		}

		private void LoadStrategies()
		{
			logger.Info("Loading strategies");
			for (int index = 0; index < settings.Strategies.Count; index++)
			{
				logger.Info("Getting strategy type");
				var type = Type.GetType(settings.Strategies[index].Type);

				logger.Info("Initializing a strategy manager");
				var manager = new StrategyManager();
				manager.Initialize(type);
				managerList.Add(manager);

				logger.Info("Loading strategy settings");
				dynamic strategy_settings = JsonConvert.DeserializeObject<ExpandoObject>(File.ReadAllText(settings.Strategies[index].Settings));
				manager.Strategy.Settings = JsonConvert.DeserializeObject(strategy_settings.Settings, manager.Strategy.SettingsType);
				manager.Strategy.Session.Instrument = instruments[strategy_settings.Symbol];
			}
		}

		private void AttachReceivers()
		{
			logger.Info("Attaching receivers");
			var broadcaster = dataLink as ITickBroadcaster;

			for (int index = 0; index < managerList.Count; index++)
			{
				broadcaster.Attach(managerList[index]);
			}
		}

		private void AttachLoggers()
		{
			for (int index = 0; index < managerList.Count; index++)
			{
				var logger = new TradeLogger();

				logger.Attach(new ConsoleLogWriter() { Index = index });
				logger.Attach(new FileLogWriter() { Index = index });

				managerList[index].Broadcaster.Attach(logger);
				loggerList.Add(index, logger);
			}
		}

		private void DetachReceivers()
		{
			logger.Info("Detaching receivers");
			var broadcaster = dataLink as ITickBroadcaster;

			for (int index = 0; index < managerList.Count; index++)
			{
				broadcaster.Detach(managerList[index]);
			}
		}

		private void DetachLoggers()
		{
			for (int index = 0; index < loggerList.Count; index++)
			{
				managerList[index].Broadcaster.Detach(loggerList[index]);
			}
		}

		private void LoadInstruments()
		{
			logger.Info("Loading instruments");
			var filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + InstrumentsFilename;

			if (!File.Exists(filename)) return;

			foreach (var item in JsonConvert.DeserializeObject<List<TradeInstrument>>(File.ReadAllText(filename)))
			{
				instruments.Add(item.Symbol, item);
			}
		}

		private void LoadSettings()
		{
			logger.Info("Loading settings");
			var filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + SettingsFilename;

			if (!File.Exists(filename)) return;

			settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filename));
		}
	}
}
