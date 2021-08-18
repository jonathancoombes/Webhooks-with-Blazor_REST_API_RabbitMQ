using SpaceXAirlines.Server.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceXAirlines.Shared.Dto;

namespace SpaceXAirlines.Server.Profiles
{
    public class WebhookSubscriptionProfile: Profile
    {
            public WebhookSubscriptionProfile()
        {
            CreateMap<WebhookSubscriptionCreateDto, WebhookSubscription>();
            CreateMap<WebhookSubscription, WebhookSubscriptionReadDto>();

        }

    }
}
