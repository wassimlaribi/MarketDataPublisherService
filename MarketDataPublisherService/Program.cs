using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace MarketDataPublisherService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			//using (var serviceScope = host.Services.CreateScope())
			//{
			//	var services = serviceScope.ServiceProvider;

			//	// get marketdataservice intance from dependency injection container 
			//	var MarketDataService = services.GetRequiredService<MarketDataService>();

			//	Task.Run(()=>MarketDataService.UpdateMarketData());
			//}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					webBuilder.UseKestrel();

				});
	}
}
