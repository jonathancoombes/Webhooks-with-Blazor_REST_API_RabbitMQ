using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgentWeb.Dtos;

namespace AirlineSendAgent.Client
{
    public interface IWebhookClient
    {
        Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayloadDto);

    }
}
