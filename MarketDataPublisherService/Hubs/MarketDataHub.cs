using MarketDataPublisherService.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketDataPublisherService
{

	//Don't store state in a property on the hub class. 
	//Every hub method call is executed on a new hub instance.
	public class MarketDataHub : Hub<IMarketDataClient>
	{
		public async Task SendMarketDataToClients(MarketData marketData)
		{
			await Clients.All.PublishMarketData(marketData);
		}

		public async Task SendMarketDataListToClients(ICollection<MarketData> marketDataList)
		{
			await Clients.All.PublishMarketDataList(marketDataList);
		}

		public Task SendClientSubscribtion(string message)
		{
			Console.WriteLine(message);
			Console.WriteLine($"Client Connection {Context.ConnectionId}");
			Console.WriteLine($"Client User {Context.User}");
			Console.WriteLine($"Client Identifier {Context.UserIdentifier}");
			return Task.FromResult("ok");
		}

		
	}
}
