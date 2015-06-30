using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingPeriod
	{
		int TickCount { get; }

		double Open { get; }

		double High { get; }

		double Low { get; }

		double Close { get; }

		double Total { get; }

		void AddTick(ITradeTick tick);

		double Value(PriceType type);

		IList<double> Ticks { get; }
	}
}
