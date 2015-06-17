using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace ElevatedTrader.Indicators
{
	public interface IIndicator
	{
		IIndicatorResult Result { get; set; }

		void Calculate(IEnumerable<ITradePeriod> periods);

		void Configure(ExpandoObject configuration);
	}
}
