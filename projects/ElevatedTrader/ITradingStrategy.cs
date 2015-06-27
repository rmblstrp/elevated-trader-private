using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradingStrategy
	{
		Dictionary<int, ITradingPeriod> Periods { get; }

		ITradingSession Session { get; }

		object Settings { get; }

		void Tick(ITradeTick tick);

		void Initialize(ITradeSymbol symbol);
	}
}
