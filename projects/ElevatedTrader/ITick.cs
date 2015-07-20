using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface ITick
	{
		DateTime Time
		{
			get;
		}

		double Price
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
