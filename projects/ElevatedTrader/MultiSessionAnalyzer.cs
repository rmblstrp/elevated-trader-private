using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class MultiSessionAnalyzer : IMultiSessionAnalyzer
	{
		public double AverageEquity
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageTotalGain
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageTotalLoss
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageHighestTradeGain
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageHighestTradeLoss
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageMaximumGain
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageMaximumLoss
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageLargestDrawdown
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageLongestDrawdownDuration
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageLargestStreak
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageLongestStreakDuration
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageTradeCount
		{
			get { throw new NotImplementedException(); }
		}

		public double AverageExpectancy
		{
			get { throw new NotImplementedException(); }
		}

		public void Analyze(ISessionAnalyzer analyzer)
		{
			throw new NotImplementedException();
		}
	}
}
