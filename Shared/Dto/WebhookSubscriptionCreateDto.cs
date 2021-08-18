using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceXAirlines.Shared.Dto
{
    public class WebhookSubscriptionCreateDto
    {          
            [Required]
            //[StringLength(300, MinimumLength = 1, ErrorMessage = "")]
            public string WebhookUri { get; set; }
            [Required]
            public string WebhookType { get; set; }

            [Required] 
            public string FlightCode { get; set; }
    }
}
