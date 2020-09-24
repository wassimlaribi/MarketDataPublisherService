using MarketDataPublisherService;
using MarketDataPublisherService.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class MarketDataServiceWorker : BackgroundService
{
    private readonly ILogger<MarketDataServiceWorker> _logger;
    private readonly IHubContext<MarketDataHub, IMarketDataClient> _marketDataHub;
    public List<MarketDataModel> MarketDataList { get; set; }
    private readonly Random Random = new Random();

    public MarketDataServiceWorker(ILogger<MarketDataServiceWorker> logger, IHubContext<MarketDataHub, IMarketDataClient> marketDataHub)
    {
        _logger = logger;
        _marketDataHub = marketDataHub;
        BuildMarketDataDefaultValue();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {Time}", DateTime.Now);

            foreach (var marketData in MarketDataList)
            {
                UpdateSpot(marketData);
                _logger.LogInformation(marketData.ToString());
            }

            _logger.LogInformation("MarketDataList", MarketDataList);

            await _marketDataHub.Clients.All.PublishMarketDataList(MarketDataList);
            await Task.Delay(1000);
        }
    }

    public void UpdateSpot(MarketDataModel marketData)
    {
        decimal ratio =  (decimal)Random.Next(-2, 2) / 100;

        marketData.PriviousSpot = marketData.Spot;
        marketData.Spot += ratio == 0 ? 0 :Math.Round(ratio * marketData.Spot,3);

        marketData.Time = DateTime.Now.ToString("HH:mm:ss");
    }

    public void BuildMarketDataDefaultValue()
    {
        MarketDataList = new List<MarketDataModel>()
            {
                 new MarketDataModel() {Ticker = "700.hk", Spot = 329.60m, Open = 330.60m, Close = 329.60m } ,

                 new MarketDataModel() {Ticker = "939.hk", Spot = 5.94m, Open = 5.60m, Close = 5.60m } ,

                 new MarketDataModel() {Ticker = "1288.HK", Spot = 3.090m, Open = 3.60m, Close = 3.60m },

                 new MarketDataModel() {Ticker = "005.K", Spot = 40.50m, Open = 41.60m, Close = 40.60m },

                 new MarketDataModel() {Ticker = "MSFT", Spot = 153.50m, Open = 150.60m, Close = 329.60m }
            };
    }
}