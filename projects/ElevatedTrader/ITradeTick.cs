using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITradeTick
	{
		DateTime Time
		{
			get;
			private set;
		}

		double Last
		{
			get;
			private set;
		}

		double Bid
		{
			get;
			private set;
		}

		double Ask
		{
			get;
			private set;
		}

		int Size
		{
			get;
			private set;
		}
	}
}
