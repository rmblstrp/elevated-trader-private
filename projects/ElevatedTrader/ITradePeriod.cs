using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradePeriod
	{
		int TickCount { get; set; }

		double Open { get; set; }

		double High { get; set; }

		double Low { get; set; }

		double Close { get; set; }

		void AddTick(ITradeTick tick);

		double Average { get; set; }
	}
}
