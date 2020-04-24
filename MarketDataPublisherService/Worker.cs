using MarketDataPublisherService;
using MarketDataPublisherService.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHubContext<MarketDataHub, IMarketDataClient> _marketDataHub;

    public Worker(ILogger<Worker> logger, IHubContext<MarketDataHub, IMarketDataClient> marketDataHub)
    {
        _logger = logger;
        _marketDataHub = marketDataHub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
            await _marketDataHub.Clients.All.PublishMarketData(new MarketData() { Ticker = "MSFT", Close = 170, Open = 171, Spot = 175});
            await Task.Delay(1000);
        }
    }
}