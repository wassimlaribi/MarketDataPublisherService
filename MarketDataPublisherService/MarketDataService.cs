using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketDataPublisherService
{

    public class MarketDataService
    {
        public List<MarketData> MarketDataList { get; set; }
        private readonly Random Random;
        private readonly IHubContext<MarketDataHub> marketDataNotifierHub;

        public MarketDataService(IHubContext<MarketDataHub> marketDataHub) 
        {
            this.marketDataNotifierHub = marketDataHub;
            BuildMarketDataDefaultValue();
            Random = new Random();
        }

        public void BuildMarketDataDefaultValue()
        {
            MarketDataList = new List<MarketData>()
            {
                 new MarketData() {Ticker = "700.hk", Spot = 329.60m, Open = 330.60m, Close = 329.60m } ,

                 new MarketData() {Ticker = "939.hk", Spot = 5.94m, Open = 330.60m, Close = 329.60m } ,

                 new MarketData() {Ticker = "1288.HK", Spot = 3.090m, Open = 330.60m, Close = 329.60m },

                 new MarketData() {Ticker = "005.K", Spot = 40.50m, Open = 330.60m, Close = 329.60m },

                 new MarketData() {Ticker = "MSFT", Spot = 153.50m, Open = 150.60m, Close = 329.60m }
            };
        }

        public void UpdateMarketData()
        {
            while (true)
            {
                foreach (var marketData in MarketDataList)
                {
                    UpdatePosition(marketData);
                    System.Diagnostics.Trace.WriteLine(marketData.ToString());
                    Console.WriteLine(marketData.ToString());
                }

                //publish notification
                marketDataNotifierHub.Clients.All.SendAsync("MarketDataNotification", MarketDataList);

                Task.Delay(1000).Wait();
            }

        }

        public void UpdatePosition(MarketData marketData)
        {
            marketData.Spot = (decimal)Math.Round(Random.NextDouble() * 1000, 3);
        }
    }

}
