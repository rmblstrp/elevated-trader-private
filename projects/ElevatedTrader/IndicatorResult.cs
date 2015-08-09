using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class IndicatorResult : ISymbolIndicatorResult
	{
		private List<double> values = new List<double>();

		public TrendDirection Direction
		{
			get;
			set;
		}

		public bool Signaled
		{
			get;
			set;
		}

		public IList<double> Values
		{
			get { return values; }
		}
	}
}
