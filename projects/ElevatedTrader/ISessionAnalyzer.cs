using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ISessionAnalyzer
	{
		double Equity { get; }

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

		double LargestStreak { get; }

		int LongestStreakDuration { get; }

		int TradeCount { get; }

		// average $ winners * win % + average $ losers * lose %
		// (average $ winners * win % + average $ losers * lose %) / -(average $ losers)
		double Expectancy { get; }

		void Analyze(ITradingSession session);
	}
}
