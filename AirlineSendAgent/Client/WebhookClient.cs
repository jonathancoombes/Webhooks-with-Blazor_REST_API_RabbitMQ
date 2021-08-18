using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TravelAgentWeb.Dtos;

namespace AirlineSendAgent.Client
{
    public class WebhookClient : IWebhookClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebhookClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayloadDto)
        {

            var serializedPayload = JsonSerializer.Serialize(flightDetailChangePayloadDto);

            var httpClient = _httpClientFactory.CreateClient();

            //Creating a new HttpRequestMessage with type + uri
            var request = new HttpRequestMessage(HttpMethod.Post, flightDetailChangePayloadDto.WebhookURI);

            //Setting the headers - good practice
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            //Setting request body
            request.Content = new StringContent(serializedPayload);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Sending the request
            try
            {
                using var response = await httpClient.SendAsync(request);
                Console.WriteLine("Success");
                response.EnsureSuccessStatusCode();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unsuccessful{ex.Message}");
            }

        }
    }
}
