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

namespace ElevatedTrader.LiveTrading.Service
{
	public class ServiceManager
	{
		private const string InstrumentsFilename = "instruments.json";
		private const string SettingsFilename = "settings.json";

		private readonly Dictionary<string, TradeInstrument> instruments = new Dictionary<string, TradeInstrument>();		
		private readonly List<StrategyManager> managerList = new List<StrategyManager>();
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
				managerList[index].Start();
			}

			AttachReceivers();
			dataLink.Connect();
		}

		private void StopStrategies()
		{
			dataLink.Disconnect();
			DetachReceivers();
			
			for (int index = 0; index < managerList.Count; index++)
			{
				managerList[index].Stop();
			}
		}

		private void LoadDataLink()
		{
			var type = Type.GetType(settings.DataLink.Type);
			dataLink = (ITradeDataLink)Activator.CreateInstance(type);

			if (dataLink as IConfigurable != null)
			{
				((IConfigurable)dataLink).Configure(settings.DataLink.Settings);
			}
		}

		private void LoadStrategies()
		{
			for (int index = 0; index < settings.Strategies.Count; index++)
			{
				var type = Type.GetType(settings.Strategies[index].Type);
				var manager = new StrategyManager();

				manager.Initialize(type);
				managerList.Add(manager);

				dynamic strategy_settings = JsonConvert.DeserializeObject<ExpandoObject>(File.ReadAllText(settings.Strategies[index].Settings));
				manager.Strategy.Settings = JsonConvert.DeserializeObject(strategy_settings.Settings, manager.Strategy.SettingsType);
				manager.Strategy.Session.Instrument = instruments[strategy_settings.Symbol];
			}
		}

		private void AttachReceivers()
		{
			var broadcaster = dataLink as ITickBroadcaster;

			for (int index = 0; index < managerList.Count; index++)
			{
				broadcaster.Attach(managerList[index]);
			}
		}

		private void DetachReceivers()
		{
			var broadcaster = dataLink as ITickBroadcaster;

			for (int index = 0; index < managerList.Count; index++)
			{
				broadcaster.Detach(managerList[index]);
			}
		}

		private void LoadInstruments()
		{
			var filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + InstrumentsFilename;

			if (!File.Exists(filename)) return;

			foreach (var item in JsonConvert.DeserializeObject<List<TradeInstrument>>(File.ReadAllText(filename)))
			{
				instruments.Add(item.Symbol, item);
			}
		}

		private void LoadSettings()
		{
			var filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + SettingsFilename;

			if (!File.Exists(filename)) return;

			settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filename));
		}
	}
}
