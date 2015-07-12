using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ElevatedTrader.Windows.Forms
{
	public class TraderSettings
	{
		public string ConnectionString
		{
			get;
			set;
		}

		public int TickDataCount
		{
			get;
			set;
		}

		const string Filename = "settings.json";

		private TraderSettings()
		{
		}

		public static TraderSettings Load()
		{
			try
			{
				if (File.Exists(Filename))
				{
					return JsonConvert.DeserializeObject<TraderSettings>(File.ReadAllText(Filename));
				}
			}
			catch { }

			return new TraderSettings();
		}

		public static void Save(TraderSettings settings)
		{
			File.WriteAllText(Filename, JsonConvert.SerializeObject(settings));
		}
	}
}
