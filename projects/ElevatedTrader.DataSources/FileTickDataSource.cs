using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElevatedTrader.DataSources
{
	public class FileTickDataSource : ITickDataSource
	{
		private string path;
		private List<ITick> ticks = new List<ITick>();
		private Regex expression = new Regex(@"\w+", RegexOptions.IgnoreCase | RegexOptions.Compiled);


		public IList<ITick> Ticks
		{
			get
			{
				return ticks;
			}
		}

		public void Clear()
		{
			ticks.Clear();
		}

		public void Configure(dynamic configuration)
		{
			path = configuration.Path;

			if (!path.EndsWith(@"\"))
			{
				path += @"\";
			}
		}

		public void Load(string symbol, int? count = null, Func<ITick, bool> added = null)
		{
			if (count == null) count = int.MaxValue;

			if (count < ticks.Count) return;

			ticks.Capacity = count.Value;

			using (var reader = new StreamReader(new FileStream(path + GetSymbolFilename(symbol), FileMode.Open, FileAccess.Read)))
			{
				var desired_count = count - ticks.Count;
				var skip_count = count - desired_count;

				if (skip_count > 0)
				{
					for (int index = 0; index < skip_count && !reader.EndOfStream; index++)
					{
						reader.ReadLine();
					}
				}

				for (int index = 0; index < desired_count && !reader.EndOfStream; index++)
				{
					var data = reader.ReadLine().Split(',');

					var tick = new Tick()
					{
						Price = double.Parse(data[1]),
						Bid = double.Parse(data[3]),
						Ask = double.Parse(data[4])
					};

					ticks.Add(tick);

					if (added != null && !added(tick))
					{
						break;
					}
				}

				reader.Close();
			}
		}

		protected string GetSymbolFilename(string symbol)
		{
			var match = expression.Match(symbol);

			return match.Value + ".csv";
		}
	}
}
