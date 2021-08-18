﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceXAirlines.Server.Models
{
    public class WebhookSubscription
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string WebhookUri { get; set; }
        [Required]
        public string Secret { get; set; }
        [Required]
        public string WebhookType { get; set; }
        [Required]
        public string  WebhookPublisher { get; set; }
        [Required]
        public string  FlightCode { get; set; }


    }
}
