using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using SpaceXAirlines.Client.Shared;
using SpaceXAirlines.Shared.Dto;


namespace SpaceXAirlines.Client.Pages
{
    public partial class Webhook
    {
        static FlightDetailReadDto flightData { get; set; }
        List<FlightDetailReadDto> Flights = new();
        WebhookSubscriptionCreateDto _dtoPost = new();
        WebhookSubscriptionReadDto _dtoRead = new();
        private bool disabled;

        string exceptionMessage;

        [CascadingParameter(Name="dto")]
        public NotificationMessageDto notifDto { get; set; } = new();
        
        protected override async Task OnInitializedAsync() =>
        Flights = await _http.GetFromJsonAsync<List<FlightDetailReadDto>>("api/flight");

        string BodyText = "Select the flight and enter your URI below to receive instant notifications when changes occur in our flight data";
        string TitleText = "Welcome to the Webhook Registration Page";
        string HeaderText = "REGISTER FLIGHT DATA WEBHOOK";
        string successMessage = "WebHook Registered! Please store the secret to validate inbound requests: ";

        string alertSuccessDisplay = ConfigurationSettings.cssHide;
        string alertDangerDisplay = ConfigurationSettings.cssHide;
        string testingButton = ConfigurationSettings.cssHide;
        string invisibleStyle = ConfigurationSettings.cssHide;
        string DisplayRegBtn = ConfigurationSettings.cssShow;
        string visibleStyle = ConfigurationSettings.cssShow;
       
        public async void RegisterWebhook()
        {
            if (Uri.IsWellFormedUriString(_dtoPost.WebhookUri, UriKind.Absolute))
            {
               try
               {
                    var response = await _http.PostAsJsonAsync(ConfigurationSettings.PostUrl, _dtoPost);
                    _dtoRead = await response.Content.ReadFromJsonAsync<WebhookSubscriptionReadDto>();

                    if (response.IsSuccessStatusCode && _dtoRead != null)
                    {
                        successMessage += $"{_dtoRead.Secret}";
                        alertSuccessDisplay = visibleStyle;
                        testingButton = visibleStyle;

                        flightData = Flights.Find(fl => fl.FlightCode == _dtoPost.FlightCode);

                        notifDto.FlightCode = flightData.FlightCode;
                        notifDto.OldPrice = flightData.Price;
                        StateHasChanged();
                    }
               }
               catch (NotSupportedException exception) 
               {
                    exceptionMessage = exception.Message;
               }
                
            }
        }
    }
    
}

