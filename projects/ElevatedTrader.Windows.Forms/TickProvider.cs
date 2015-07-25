using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader.Windows.Forms
{
	public static class TickProvider
	{
		private static Dictionary<string, Type> providers = new Dictionary<string, Type>();

		public static IList<string> Providers
		{
			get
			{
				return providers.Keys.ToList();
			}
		}

		static TickProvider()
		{
			AddProvider<HistoricalTickProvider>();
			AddProvider<MonteCarloTickProvider>();
		}

		public static void AddProvider<T>() where T: ITickProvider, new()
		{
			AddProvider(typeof(T));
		}

		public static void AddProvider(Type type)
		{
			providers.Add(type.Name, type);
		}

		public static ITickProvider Create(string name)
		{
			if (!providers.ContainsKey(name))
			{
				throw new KeyNotFoundException(name);
			}

			var instance = (ITickProvider)Activator.CreateInstance(providers[name]);

			return instance;
		}
	}
}
