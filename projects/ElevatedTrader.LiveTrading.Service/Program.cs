using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ElevatedTrader.LiveTrading.Service
{
	class Program
	{
		static void Main(string[] args)
		{
			HostFactory.Run(x =>
			{
				x.UseNLog();

				x.Service<ServiceManager>(s =>
				{
					s.ConstructUsing(name => new ServiceManager());
					s.WhenStarted(service => service.Start());
					s.WhenStopped(service => service.Stop());
				});

				x.RunAsLocalSystem();
				x.EnableServiceRecovery(sr => sr.RestartService(1));

				x.SetDisplayName("Elevated Trader Strategy Service");
				x.SetServiceName("ElevatedTrader");
				//x.DependsOnMsSql();
			});
		}
	}
}
