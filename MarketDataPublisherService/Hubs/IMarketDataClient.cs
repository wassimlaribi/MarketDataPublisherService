using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketDataPublisherService.Hubs
{
	public interface IMarketDataClient
	{
		//Task MarketDataNotification(ICollection<MarketData> marketDatas);
		public Task PublishMarketData(MarketData marketData);
	}
}
