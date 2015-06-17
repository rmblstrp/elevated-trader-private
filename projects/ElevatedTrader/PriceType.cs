using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	[Flags]
	public enum PriceType
	{
		Open			= 0x0001,
		High			= 0x0002,
		Low				= 0x0004,
		Close			= 0x0008,
		OHLC			= Open | High | Low | Close,
		OpenClose		= Open | Close,
		HighLow			= High | Low,
		HighLowClose	= High | Low,
	}
}
