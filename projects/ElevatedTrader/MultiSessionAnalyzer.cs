using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class MultiSessionAnalyzer : IMultiSessionAnalyzer
	{
		private int sessionAnalysisCount;

		public int SessionCount
		{
			get { return sessionAnalysisCount; }
		}

		public double AverageProfitLoss
		{
			get;
			protected set;
		}

		public double AverageTotalGain
		{
			get;
			protected set;
		}

		public double AverageTotalLoss
		{
			get;
			protected set;
		}

		public double AverageLargestGain
		{
			get;
			protected set;
		}

		public double AverageLargestLoss
		{
			get;
			protected set;
		}

		public double AverageMaximumGain
		{
			get;
			protected set;
		}

		public double AverageMaximumLoss
		{
			get;
			protected set;
		}

		public double AverageLargestDrawdown
		{
			get;
			protected set;
		}

		public double AverageLongestDrawdown
		{
			get;
			protected set;
		}

		public double AverageLargestRunup
		{
			get;
			protected set;
		}

		public double AverageLongestRunup
		{
			get;
			protected set;
		}

		public double AverageTradeCount
		{
			get;
			protected set;
		}

		public double AverageTradeProfitLoss
		{
			get;
			protected set;
		}

		public double AverageWinRatio
		{
			get;
			protected set;
		}

		public double AverageLossRatio
		{
			get;
			protected set;
		}

		public double AverageRiskRewardRatio
		{
			get;
			protected set;
		}

		public double AverageExpectancy
		{
			get;
			protected set;
		}

		public void Analyze(ISessionAnalyzer analyzer)
		{
			sessionAnalysisCount++;

			var divisor = sessionAnalysisCount == 1 ? 1 : 2;

			AverageProfitLoss = Math.Round((AverageProfitLoss + analyzer.ProfitLoss) / divisor, 2);

			AverageTotalGain = Math.Round((AverageTotalGain + analyzer.TotalGain) / divisor, 2);

			AverageTotalLoss = Math.Round((AverageTotalLoss + analyzer.TotalLoss) / divisor, 2);

			AverageLargestGain = Math.Round((AverageLargestGain + analyzer.LargestGain) / divisor, 2);

			AverageLargestLoss = Math.Round((AverageLargestLoss + analyzer.LargestLoss) / divisor, 2);

			AverageMaximumGain = Math.Round((AverageMaximumGain + analyzer.MaximumEquity) / divisor, 2);

			AverageMaximumLoss = Math.Round((AverageMaximumLoss + analyzer.MinimumEquity) / divisor, 2);

			AverageLargestDrawdown = Math.Round((AverageLargestDrawdown + analyzer.LargestDrawdown) / divisor, 2);

			AverageLongestDrawdown = Math.Round((AverageLongestDrawdown + analyzer.LongestDrawdown) / divisor, 2);

			AverageLargestRunup = Math.Round((AverageLargestRunup + analyzer.LargestRunup) / divisor, 2);

			AverageLongestRunup = Math.Round((AverageLongestRunup + analyzer.LongestRunup) / divisor, 2);

			AverageTradeCount = Math.Round((AverageTradeCount + analyzer.TradeCount) / divisor, 2);

			AverageTradeProfitLoss = Math.Round((AverageTradeProfitLoss + analyzer.TradeProfitLoss) / divisor, 2);

			AverageWinRatio = Math.Round((AverageWinRatio + analyzer.WinRatio) / divisor, 2);

			AverageLossRatio = Math.Round((AverageLossRatio + analyzer.LossRatio) / divisor, 2);

			AverageRiskRewardRatio = Math.Round((AverageRiskRewardRatio + analyzer.RiskRewardRatio) / divisor, 2);

			AverageExpectancy = Math.Round((AverageExpectancy + analyzer.Expectancy) / divisor, 2);
		}
	}
}
