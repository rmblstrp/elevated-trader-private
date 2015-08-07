using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface IMultiSessionAnalyzer
	{
		double AverageEquity { get; }

		double AverageTotalGain { get; }

		double AverageTotalLoss { get; }

		double AverageHighestTradeGain { get; }

		double AverageHighestTradeLoss { get; }

		double AverageMaximumGain { get; }

		double AverageMaximumLoss { get; }

		double AverageLargestDrawdown { get; }

		double AverageLongestDrawdownDuration { get; }

		double AverageLargestStreak { get; }

		double AverageLongestStreakDuration { get; }

		double AverageTradeCount { get; }

		// average $ winners * win % + average $ losers * lose %
		// (average $ winners * win % + average $ losers * lose %) / -(average $ losers)
		double AverageExpectancy { get; }

		void Analyze(ISessionAnalyzer analyzer);
	}
}
