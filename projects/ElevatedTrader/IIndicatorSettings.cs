using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatedTrader
{
	public interface IIndicatorSettings
	{
		object Settings { get; set; }
	}

	public interface IIndicatorSettings<T> : IIndicatorSettings
	{
		new T Settings { get; set; }
	}
}
