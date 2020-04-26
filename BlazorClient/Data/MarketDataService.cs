using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Data
{
	public class MarketDataService
	{

		public Task<List<MarketData>> GetMarketDataAsync()
		{
			//var rng = new Random();
			//return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
			//{
			//	Date = startDate.AddDays(index),
			//	TemperatureC = rng.Next(-20, 55),
			//	Summary = Summaries[rng.Next(Summaries.Length)]
			//}).ToArray());
			return Task.FromResult(new List<MarketData>());
		}
	}
}
