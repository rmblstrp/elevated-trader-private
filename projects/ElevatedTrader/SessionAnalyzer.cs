using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class SessionAnalyzer : ISessionAnalyzer
	{
		public double ProfitLoss
		{
			get;
			protected set;
		}

		public double TotalGain
		{
			get;
			protected set;
		}

		public double TotalLoss
		{
			get;
			protected set;
		}
		public double LargestGain
		{
			get;
			protected set;
		}

		public double LargestLoss
		{
			get;
			protected set;
		}

		public double MaximumEquity
		{
			get;
			protected set;
		}

		public double MinimumEquity
		{
			get;
			protected set;
		}

		public double LargestRunup
		{
			get;
			protected set;
		}

		public double LargestDrawdown
		{
			get;
			protected set;
		}

		public int LongestRunup
		{
			get;
			protected set;
		}

		public int LongestDrawdown
		{
			get;
			protected set;
		}

		public int TradeCount
		{
			get;
			protected set;
		}

		public int TradeWins
		{
			get;
			protected set;
		}

		public int TradeLosses
		{
			get;
			protected set;
		}

		public double WinRatio
		{
			get;
			protected set;
		}

		public double LossRatio
		{
			get;
			protected set;
		}

		public double RiskRewardRatio
		{
			get;
			protected set;
		}

		public double Expectancy
		{
			get;
			protected set;
		}

		public void Analyze(ITradingSession session)
		{
			Reset();

			var trades = session.Trades;

			if (trades.Count == 0) return;

			ProfitLoss = Math.Round(trades.Last().Equity, 2);
			TotalGain = Math.Round(trades.Sum(x => x.Profit > 0 ? x.Profit : 0), 2);
			TotalLoss = Math.Round(trades.Sum(x => x.Profit < 0 ? x.Profit : 0), 2);
			LargestGain = Math.Round(trades.Max(x => x.Profit), 2);
			LargestLoss = Math.Round(trades.Min(x => x.Profit), 2);
			MaximumEquity = Math.Round(trades.Max(x => x.Equity), 2);
			MinimumEquity = Math.Round(trades.Min(x => x.Equity), 2);
			TradeCount = trades.Count;
			TradeWins = trades.Sum(x => x.Profit > 0 ? 1 : 0);
			TradeLosses = trades.Sum(x => x.Profit < 0 ? 1 : 0);

			int drawdown_duration = 0, runup_duration = 0;
			double drawdown = 0, runup = 0;

			foreach (var trade in trades)
			{
				if (trade.Profit > 0)
				{
					LargestDrawdown = Math.Round(Math.Min(LargestDrawdown, drawdown), 2);
					LongestDrawdown = Math.Max(LongestDrawdown, drawdown_duration);
					drawdown_duration = 0;
					drawdown = 0;

					runup_duration++;
					runup += trade.Profit;
				}
				else if (trade.Profit < 0)
				{
					LargestRunup = Math.Round(Math.Max(LargestRunup, runup), 2);
					LongestRunup = Math.Max(LongestRunup, runup_duration);
					runup_duration = 0;
					runup = 0;

					drawdown_duration++;
					drawdown += trade.Profit;
				}
			}

			WinRatio = TradeWins / (double)TradeCount;
			LossRatio = TradeLosses / (double)TradeCount;
			RiskRewardRatio = (TotalGain / (double)TradeWins) / Math.Abs(TotalLoss / (double)TradeLosses);
			Expectancy = Math.Round(RiskRewardRatio * WinRatio - LossRatio, 2);

			WinRatio = Math.Round(WinRatio, 2);
			LossRatio = Math.Round(LossRatio, 2);
			RiskRewardRatio = Math.Round(RiskRewardRatio, 2);
		}

		protected void Reset()
		{
			ProfitLoss = 0;
			TotalGain = 0;
			TotalLoss = 0;
			LargestGain = 0;
			LargestLoss = 0;
			MaximumEquity = 0;
			MinimumEquity = 0;
			LargestDrawdown = 0;
			LongestDrawdown = 0;
			LargestRunup = 0;
			LongestRunup = 0;
			TradeCount = 0;
			TradeWins = 0;
			TradeLosses = 0;
			Expectancy = 0;
		}
	}
}
