using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class TradeTickAggregator : ITradeTickAggregator
	{
		protected ITradeTick last;
		protected Dictionary<int, ITradingPeriod> periods = new Dictionary<int, ITradingPeriod>();
		protected List<int> sizes = new List<int>();

		public IDictionary<int, int> Indexes
		{
			get { throw new NotImplementedException(); }
		}

		public ITradeTick Last
		{
			get { return last; }
		}

		public IDictionary<int, ITradingPeriod> Periods
		{
			get { return periods; }
		}

		public IList<int> Sizes
		{
			get { return sizes; }
		}

		public event Action<int> BeforeNewPeriod;

		public void Add(ITradeTick tick)
		{
			last = tick;
		}

		public void Reset()
		{
			periods.Clear();
		}
	}
}
