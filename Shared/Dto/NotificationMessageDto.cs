using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Shared.Dto
{
    public class NotificationMessageDto
    {

        public NotificationMessageDto()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string WebhookType { get; set; }
        public string FlightCode { get; set; }
        public decimal OldPrice { get; set; }

    }
}
