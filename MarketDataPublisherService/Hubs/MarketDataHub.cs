﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MarketDataPublisherService
{

	//Don't store state in a property on the hub class. 
	//Every hub method call is executed on a new hub instance.
	public class MarketDataHub : Hub
	{

		public Task SendClientSubscribtion(string user,string message)
		{
			Console.WriteLine(message);
			Console.WriteLine($"Client Connection {Context.ConnectionId}");
			Console.WriteLine($"Client User {Context.User}");
			Console.WriteLine($"Client Identifier {Context.UserIdentifier}");
			return Task.FromResult("ok");
		}

		//public Task SendMessageToCaller(string message)
		//{
		//	return Clients.Caller.SendAsync("ReceiveMessage", message);
		//}

		//public Task SendMessageToGroup(string message)
		//{
		//	return Clients.Group("SignalR Users").SendAsync("ReceiveMessage", message);
		//}
	}
}