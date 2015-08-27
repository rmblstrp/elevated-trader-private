using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingPeriodAggregator : ITradingPeriodAggregator
	{
		protected ITick last;
		protected Dictionary<int, IList<ITradingPeriod>> periods = new Dictionary<int, IList<ITradingPeriod>>();
		protected List<int> sizes = new List<int>();

		public ITick Last
		{
			get { return last; }
		}

		public IDictionary<int, IList<ITradingPeriod>> Periods
		{
			get { return periods; }
		}

		public event Action<int> AfterNewPeriod;
		public event Action<int> BeforeNewPeriod;

		public void AddSize(int size, int capacity)
		{
			var list = new List<ITradingPeriod>(capacity);
			periods.Add(size, list);
			sizes.Add(size);
			AddNewPeriod(size);
		}

		public void AddQuote(ITradeQuote quote)
		{
			foreach (var item in periods)
			{
				var period = item.Value[item.Value.Count - 1];

				period.AddQuote(quote);
			}
		}

		public void AddTick(ITick tick)
		{
			last = tick;

			foreach (var item in periods)
			{
				var period = item.Value[item.Value.Count - 1];

				if (period.TickCount == item.Key)
				{
					DoBeforeNewPeriod(item.Key);

					AddNewPeriod(item.Key);
					period.AddTick(tick);

					DoAfterNewPeriod(item.Key);
				}
				else
				{
					period.AddTick(tick);
				}
			}
		}

		protected void AddNewPeriod(int key)
		{
			periods[key].Add(new TradingPeriod());
		}

		protected void DoAfterNewPeriod(int size)
		{
			if (AfterNewPeriod != null)
			{
				AfterNewPeriod(size);
			}
		}

		protected void DoBeforeNewPeriod(int size)
		{
			if (BeforeNewPeriod != null)
			{
				BeforeNewPeriod(size);
			}
		}

		public ITradingPeriod CurrentPeriod(int size)
		{
			var item = periods[size];

			return item[item.Count - 1];
		}

		public IDictionary<int, int> Indexes()
		{
			var result = new Dictionary<int, int>();

			foreach (var item in periods)
			{
				result.Add(item.Key, item.Value.Count - 1);
			}

			return result;
		}

		public void Clear()
		{
			periods.Clear();
			sizes.Clear();
		}

		public void FreeResources(int keep = 0)
		{
			var key_list = periods.Keys.ToList();

			for (int index = 0; index < periods.Count; index++)
			{
				var key = key_list[index];

				var list = new List<ITradingPeriod>();
				var old = periods[key];

				if (old.Count < keep) continue;

				for (int idx = old.Count - keep - 1; idx < old.Count; idx++)
				{
					list.Add(old[idx]);
				}

				old.Clear();
				periods[key] = list;
			}
		}
	}
}
