using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketDataPublisherService.Hubs
{
	public interface IMarketDataClient
	{
		public Task PublishMarketData(MarketData marketData);
		public Task PublishMarketDataList(ICollection<MarketData> marketDataCollection);
	}
}
