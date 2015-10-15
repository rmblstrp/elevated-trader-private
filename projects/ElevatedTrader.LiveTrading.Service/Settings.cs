using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader.LiveTrading.Service
{
	public class Settings
	{
		public class InfoDefinition<T>
		{
			public string Type
			{
				get;
				set;
			}

			public T Settings
			{
				get;
				set;
			}
		}

		public InfoDefinition<ExpandoObject> DataLink
		{
			get;
			set;
		}

		public List<InfoDefinition<string>> Strategies
		{
			get;
			set;
		}
	}
}
