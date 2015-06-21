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
		}

		double Last
		{
			get;
		}

		double Bid
		{
			get;
		}

		double Ask
		{
			get;
		}

		int Size
		{
			get;
		}
	}
}
