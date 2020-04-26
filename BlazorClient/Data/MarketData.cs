using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Data
{
	public class MarketData
	{
		public string Ticker { get; set; }
		public decimal Spot { get; set; }
		public decimal Open { get; set; }
		public decimal Close { get; set; }
		public string Time { get; set; }
		public override string ToString()
		{
			return $"Ticket :{Ticker} , Spot :" +
					$"{Spot}, Open : {Open} , Close : {Close}, Time {Time} ";
		}

	}
}
