using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	[Flags]
	public enum PeriodValueType
	{
		Average				= 0x0000,
		Open				= 0x0001,
		High				= 0x0002,
		Low					= 0x0004,
		Close				= 0x0008,
		GeometricMean		= 0x0010,
		WeightedAverage		= 0x0020,
		HarmonicMean		= 0x0040,
		Median				= 0x0080,
		Skewness			= 0x0100,
		Variance			= 0x0200,
		Kurtosis			= 0x0400,
		StandardDeviation	= 0x1000,
		OHLC				= Open | High | Low | Close,
		OpenClose			= Open | Close,
		HighLow				= High | Low,
		HighLowClose		= High | Low | Close,
	}
}
