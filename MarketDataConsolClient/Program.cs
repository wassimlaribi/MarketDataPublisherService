using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
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

				// Notify users the connection was lost and the client is reconnecting.
				// Start queuing or dropping messages.

				return Task.CompletedTask;
			};

			connection.Reconnected += connectionId =>
			{
				Debug.Assert(connection.State == HubConnectionState.Connected);

				// Notify users the connection was reestablished.
				// Start dequeuing messages queued while reconnecting if any.

				return Task.CompletedTask;
			};

			//on market data notification received 
			connection.On<object>("MarketDataNotification", (obj) =>
			{
				Console.WriteLine($"message received {obj}");
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
