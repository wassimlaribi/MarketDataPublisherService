using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketDataPublisherService.Hubs
{
	public interface IMarketDataClient
	{
		public Task PublishMarketDataList(ICollection<MarketDataModel> marketDataCollection);
	}
}
