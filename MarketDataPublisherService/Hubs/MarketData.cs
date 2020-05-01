using System;

namespace MarketDataPublisherService
{
	public class MarketData
	{
        public string Ticker { get; set; }
        public decimal Spot { get; set; }

        public decimal PriviousSpot { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }

        public string Time { get; set; }

        public override string ToString()
        {
            return  $"Ticket :{Ticker} , Spot :" +
                    $"{Spot}, Open : {Open} , Close : {Close} , Time {Time}";
        }
       
    }
}