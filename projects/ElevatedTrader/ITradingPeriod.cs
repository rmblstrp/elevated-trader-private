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

		IList<double> Quotes { get; }

		IList<double> Ticks { get; }

		IList<double> Changes { get; }

		double EfficiencyRatio { get; }

		void AddQuote(ITradeQuote quote);

		void AddTick(ITick tick);		

		double PeriodValue(PeriodValueType type);

		double QuoteValue(PeriodValueType type);
	}
}
