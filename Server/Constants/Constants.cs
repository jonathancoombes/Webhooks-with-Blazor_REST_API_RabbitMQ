using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Server.Constants
{    public static class Constants
    {

        public static readonly string AirLine = "SpaceXAirlines";
        
        public enum WebhookType{
            PriceChange = 1,
            SeatAvailability = 2
        }

    }
}
