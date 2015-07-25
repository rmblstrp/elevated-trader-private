using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ElevatedTrader.Windows.Forms
{
	public static class ApplicationSettings
	{
		private const string Filename = "settings.json";
		private static Settings settings;

		private class Settings
		{
			public int MaxTickCount
			{
				get;
				set;
			}
		}

		public static int MaxTickCount
		{
			get
			{
				return settings.MaxTickCount;
			}
			set
			{
				settings.MaxTickCount = value;
			}
		}

		static ApplicationSettings()
		{
			try
			{
				if (File.Exists(Filename))
				{
					settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Filename));
				}
			}
			catch
			{
				settings = new Settings();
			}			
		}

		public static void Save()
		{
			File.WriteAllText(Filename, JsonConvert.SerializeObject(settings));
		}
	}
}
