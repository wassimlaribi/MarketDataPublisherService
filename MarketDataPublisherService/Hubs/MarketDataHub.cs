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

		public async Task SendMarketDataListToClients(ICollection<MarketDataModel> marketDataList)
		{
			await Clients.All.PublishMarketDataList(marketDataList);
		}
		
	}
}
