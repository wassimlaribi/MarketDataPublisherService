using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MarketDataConsolClient
{
	class Program
	{
		
		static void Main(string[] args)
		{
			//Build signalR connection
			HubConnection connection = new HubConnectionBuilder()
				.WithUrl("http://localhost:5000/marketDataHub") 
				.WithAutomaticReconnect() //handel lost connection
				.Build();

			Console.WriteLine("Main thread ID" +Thread.CurrentThread.ManagedThreadId);

			//on close event 
			connection.Closed += async (error) =>
			{
				await Task.Delay(new Random().Next(0, 5) * 1000);
				await connection.StartAsync();
			};

			connection.Reconnecting += error =>
			{
				Debug.Assert(connection.State == HubConnectionState.Reconnecting);
				Debug.WriteLine("Connection lost, trying to reconnect");

				return Task.CompletedTask;
			};

			connection.Reconnected += connectionId =>
			{
				Debug.Assert(connection.State == HubConnectionState.Connected);
			
				return Task.CompletedTask;
			};

			//on market data notification received 
			connection.On<object>("PublishMarketDataList", (marketData) =>
			{
				Console.WriteLine("on market data received thread ID"+Thread.CurrentThread.ManagedThreadId);
				Console.WriteLine($"message received {marketData}");
			});

			//Open client server connection
			connection.StartAsync();

			//call server from client 
			connection.InvokeAsync("SendClientSubscribtion", "user1", "hello from client");
			

			Console.WriteLine("Hello World!");

			Console.ReadKey();
		}
	}
}
