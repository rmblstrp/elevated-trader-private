using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradingPeriodAggregator : ITradingPeriodAggregator
	{
		protected ITradeTick last;
		protected Dictionary<int, IList<ITradingPeriod>> periods = new Dictionary<int, IList<ITradingPeriod>>();
		protected List<int> sizes = new List<int>();

		public ITradeTick Last
		{
			get { return last; }
		}

		public IDictionary<int, IList<ITradingPeriod>> Periods
		{
			get { return periods; }
		}

		public event Action<int> BeforeNewPeriod;

		public void AddSize(int size, int capacity)
		{
			var list = new List<ITradingPeriod>(capacity);
			periods.Add(size, list);
			AddNewPeriod(size);
		}

		public void AddTick(ITradeTick tick)
		{
			last = tick;

			foreach (var item in periods)
			{
				var period = item.Value[item.Value.Count - 1];

				if (period.TickCount == item.Key)
				{
					DoBeforeNewPeriod(item.Key);
					AddNewPeriod(item.Key);
				}
			}
		}

		protected void AddNewPeriod(int key)
		{
			periods[key].Add(new TradingPeriod());
		}

		protected void DoBeforeNewPeriod(int size)
		{
			if (BeforeNewPeriod != null)
			{
				BeforeNewPeriod(size);
			}
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

		public void Reset()
		{
			periods.Clear();

			for (int index = 0; index < sizes.Count; index++)
			{
				AddNewPeriod(sizes[index]);
			}
		}
	}
}
