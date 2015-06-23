using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingStrategy
	{
		IList<IIndicator> Indicators { get; }

		object Settings { get; set; }

		void AddPeriod();

		void Tick(ITradePeriod period);
	}
}
