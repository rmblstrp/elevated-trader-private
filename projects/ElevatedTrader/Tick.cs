using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public class Tick : ITick
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

		public static TickDelta operator -(Tick a, ITick b)
		{
			return new TickDelta()
			{
				Price = a.Price - b.Price,
				Bid = a.Price - a.Bid,
				Ask = a.Price - a.Ask
			};
		}
	}
}
