using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorClient.Data
{
	public class MarketDataService
	{
		public event EventHandler MarketDataReceived;
		public List<MarketData> MarketDataList { get; set; } = new List<MarketData>();
		public ILogger<MarketDataService> Logger { get; }

		public MarketDataService(ILogger<MarketDataService> logger)
		{
			Logger = logger;
		}

		public void BuildServerConnection()
		{
			HubConnection connection = new HubConnectionBuilder()
				.WithUrl("http://localhost:5000/marketDataHub")
				.WithAutomaticReconnect() //handel lost connection
				.Build();

			Logger.LogInformation("Main thread ID" + Thread.CurrentThread.ManagedThreadId);

			//on close event
			connection.Closed += async (error) =>
			{
				await Task.Delay(new Random().Next(0, 5) * 1000);
				await connection.StartAsync();
			};

			connection.Reconnecting += error =>
			{
				Debug.Assert(connection.State == HubConnectionState.Reconnecting);
				Logger.LogInformation("Connection lost, trying to reconnect");
				return Task.CompletedTask;
			};

			connection.Reconnected += connectionId =>
			{
				Debug.Assert(connection.State == HubConnectionState.Connected);
				return Task.CompletedTask;
			};

			//on market data notification received
			connection.On<List<MarketData>>("PublishMarketDataList", (data) => UpdateStockGrid(data));

			//Open client server connection
			connection.StartAsync();
		}

		public void OnMarketDataReceived(EventArgs e)
		{
			MarketDataReceived?.Invoke(this, e);
		}

		private void UpdateStockGrid(List<MarketData> marketData)
		{
			Logger.LogInformation("Market data received thread ID" + Thread.CurrentThread.ManagedThreadId);

			MarketDataList = marketData;
			//Notify UI  
			OnMarketDataReceived(new EventArgs());
		}
	}

}
