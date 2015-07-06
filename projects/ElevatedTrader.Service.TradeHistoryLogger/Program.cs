using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ElevatedTrader.Service.TradeHistoryLogger
{
	class Program
	{
		static void Main(string[] args)
		{
			HostFactory.Run(x =>
			{
				x.UseNLog();

				x.Service<Processor>(s =>
				{
					s.ConstructUsing(name => new Processor());
					s.WhenStarted(service => service.Start());
					s.WhenStopped(service => service.Stop());
				});

				x.RunAsLocalSystem();
				x.EnableServiceRecovery(sr => sr.RestartService(1));

				x.SetDisplayName("Elevated Trade History Logger");
				x.SetServiceName("ElevatedTradeHistoryLogger");
				//x.DependsOnMsSql();
			});
		}
	}
}
