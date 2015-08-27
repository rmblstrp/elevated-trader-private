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

		int LongestRunup { get; }

		int LongestDrawdown { get; }		

		int TradeCount { get; }

		int TradeWins { get; }

		int TradeLosses { get; }

		double TradeProfitLoss { get; }

		double WinRatio { get; }

		double LossRatio { get; }

		double RiskRewardRatio { get; }

		double Expectancy { get; }

		void Analyze(ITradingSession session);
	}
}
