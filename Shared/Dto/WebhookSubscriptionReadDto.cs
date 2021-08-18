using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Shared.Dto
{
    public class WebhookSubscriptionReadDto
    {
        public int Id { get; set; }
        public string WebhookUri { get; set; }
        public string Secret { get; set; }
        public string WebhookType { get; set; }
        public string WebhookPublisher { get; set; }
        public string FlightCode { get; set; }

    }
}
