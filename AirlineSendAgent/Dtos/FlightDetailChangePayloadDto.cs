using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentWeb.Dtos
{
    public class FlightDetailChangePayloadDto
    {
        public string Publisher { get; set; }
        public string Secret { get; set; }
        public string FlightCode { get; set; }
        public decimal OldPrice { get; set;}
        public decimal NewPrice { get; set; }
        public string WebhookType { get; set; }
        public string WebhookURI{ get; set; }

    }
}
