using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ISessionAnalyzer
	{
		double ProfitLoss { get; }

		double TotalGain { get; }

		double TotalLoss { get; }

		double LargestGain { get; }

		double LargestLoss { get; }

		// Max value above 0
		double MaximumEquity { get; }

		// Max value below 0
		double MinimumEquity { get; }

		double LargestRunup { get; }

		double LargestDrawdown { get; }

		int LongestRunupDuration { get; }

		int LongestDrawdownDuration { get; }		

		int TradeCount { get; }

		int TradeWins { get; }

		int TradeLosses { get; }

		double WinRatio { get; }

		double LossRatio { get; }

		double RiskRewardRatio { get; }

		// average $ winners * win % + average $ losers * lose %
		// (average $ winners * win % + average $ losers * lose %) / -(average $ losers)
		double Expectancy { get; }

		void Analyze(ITradingSession session);
	}
}
