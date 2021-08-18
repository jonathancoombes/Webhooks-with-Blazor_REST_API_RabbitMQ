using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineSendAgent.Dtos
{
    public class NotificationMessageDto
    {
        public string Id { get; }
        public string WebhookType { get; set; }
        public string FlightCode { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }


    }

}
