using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class SessionAnalyzer : ISessionAnalyzer
	{
		public IList<double> Equity
		{
			get;
			protected set;
		}

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
public double HighestTradeGain
		{
			get;
			protected set;
		}

		public double HighestTradeLoss
		{
			get;
			protected set;
		}

		public double MaximumGain
		{
			get;
			protected set;
		}

		public double MaximumLoss
		{
			get;
			protected set;
		}

		public double LargestDrawdown
		{
			get;
			protected set;
		}

		public int LongestDrawdownDuration
		{
			get;
			protected set;
		}

		public double LargestRunUp
		{
			get;
			protected set;
		}

		public int LongestRunUpDuration
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

		public double Expectancy
		{
			get;
			protected set;
		}

		public SessionAnalyzer()
		{
			Equity = new List<double>();
		}

		public void Analyze(ITradingSession session)
		{
			Reset();

			var trades = session.Trades;

			Equity = trades.Select(x => x.Equity).ToList();
			ProfitLoss = trades.Last().Equity;
			TotalGain = trades.Sum(x => x.Profit > 0 ? x.Profit : 0);
			TotalLoss = trades.Sum(x => x.Profit < 0 ? x.Profit : 0);
			HighestTradeGain = trades.Max(x => x.Profit);
			HighestTradeLoss = trades.Min(x => x.Profit);
			MaximumGain = trades.Max(x => x.Equity);
			MaximumLoss = trades.Min(x => x.Equity);
			TradeCount = trades.Count;
			TradeWins = trades.Sum(x => x.Profit > 0 ? 1 : 0);
			TradeLosses = trades.Sum(x => x.Profit < 0 ? 1 : 0);

			int drawdown_duration = 0, runup_duration = 0;
			double drawdown = 0, runup = 0;

			foreach (var trade in trades)
			{
				if (trade.Profit > 0)
				{
					LargestDrawdown = Math.Max(LargestDrawdown, drawdown);
					LongestDrawdownDuration = Math.Max(LongestDrawdownDuration, drawdown_duration);
					drawdown_duration = 0;
					drawdown = 0;

					runup_duration++;
					runup += trade.Profit;
				}
				else if (trade.Profit < 0)
				{
					LargestRunUp = Math.Max(LargestRunUp, runup);
					LongestRunUpDuration = Math.Max(LongestRunUpDuration, runup_duration);
					runup_duration = 0;
					runup = 0;

					drawdown_duration++;
					drawdown += trade.Profit;
				}
			}

			Expectancy =
				(TotalGain / TradeWins) * (TradeWins / (double)TradeCount) +
				(TotalLoss / TradeLosses) * (TradeLosses / (double)TradeCount) /
				(TradeLosses / (double)TradeCount);
		}

		protected void Reset()
		{
			ProfitLoss = 0;
			TotalGain = 0;
			TotalLoss = 0;
			HighestTradeGain = 0;
			HighestTradeLoss = 0;
			MaximumGain = 0;
			MaximumLoss = 0;
			LargestDrawdown = 0;
			LongestDrawdownDuration = 0;
			LargestRunUp = 0;
			LongestRunUpDuration = 0;
			TradeCount = 0;
			TradeWins = 0;
			TradeLosses = 0;
			Expectancy = 0;
		}
	}
}
