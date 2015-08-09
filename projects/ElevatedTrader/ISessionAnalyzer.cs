using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ISessionAnalyzer
	{
		IList<double> Equity { get; }

		double ProfitLoss { get; }

		double TotalGain { get; }

		double TotalLoss { get; }

		double HighestTradeGain { get; }

		double HighestTradeLoss { get; }

		// Max value above 0
		double MaximumGain { get; }

		// Max value below 0
		double MaximumLoss { get; }

		double LargestDrawdown { get; }

		int LongestDrawdownDuration { get; }

		double LargestRunUp { get; }

		int LongestRunUpDuration { get; }

		int TradeCount { get; }

		int TradeWins { get; }

		int TradeLosses { get; }

		// average $ winners * win % + average $ losers * lose %
		// (average $ winners * win % + average $ losers * lose %) / -(average $ losers)
		double Expectancy { get; }

		void Analyze(ITradingSession session);
	}
}
