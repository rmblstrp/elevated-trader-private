using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface IMultiSessionAnalyzer
	{
		int SessionCount { get; }

		double AverageProfitLoss { get; }

		double AverageTotalGain { get; }

		double AverageTotalLoss { get; }

		double AverageLargestGain { get; }

		double AverageLargestLoss { get; }

		double AverageMaximumGain { get; }

		double AverageMaximumLoss { get; }

		double AverageLargestDrawdown { get; }

		double AverageLongestDrawdown { get; }

		double AverageLargestRunup { get; }

		double AverageLongestRunup { get; }

		double AverageTradeCount { get; }

		double AverageTradeProfitLoss { get; }

		double AverageWinRatio { get; }

		double AverageLossRatio { get; }

		double AverageRiskRewardRatio { get; }
		
		double AverageExpectancy { get; }

		void Analyze(ISessionAnalyzer analyzer);
	}
}
