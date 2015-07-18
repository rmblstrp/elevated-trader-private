using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public struct TradeTick : ITradeTick
	{
		public DateTime Time
		{
			get;
			set;
		}

		public double Price
		{
			get;
			set;
		}

		public double Bid
		{
			get;
			set;
		}

		public double Ask
		{
			get;
			set;
		}

		public int Size
		{
			get;
			set;
		}
	}
}
