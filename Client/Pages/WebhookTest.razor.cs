using Microsoft.AspNetCore.Components;
using SpaceXAirlines.Client.Shared;
using SpaceXAirlines.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SpaceXAirlines.Client.Pages
{
    public partial class WebhookTest
    {

    [CascadingParameter(Name = "dto")]
    public NotificationMessageDto NotDto { get; set; } = new();

    FlightDetailUpdateDto _dtoPost = new();
    NotificationMessageDto _dtoRead = new();

    protected override void OnInitialized()
    {
        Price = NotDto.OldPrice;
    }

    decimal Price;
    bool inProcessFlag = false;

    string tick1, tick2, tick3 = String.Empty;
    string tableshow, invisibleStyle = ConfigurationSettings.cssHide;
    string itemShow, visibleStyle = ConfigurationSettings.cssShow;
    string priceChange = ConfigurationSettings.cssHide;
    string displayTick = "bi bi-check-lg";
    string alertErrorDisplay = ConfigurationSettings.cssHide;
    string submitShow = ConfigurationSettings.cssShow;

    string errorMessage = String.Empty;
    string samePriceError = "The Price has not changed. Adjust the price to submit a price update!";
    string successMessage = String.Empty;

    string HeaderText = "TESTING FLIGHT DATA WEBHOOK";
    string TitleText = "Welcome to the Webhook Testing Page";
    string BodyText = "Trigger the desired flight price change and view the results delivered to the designated URI below";


    //Price Update and Webhook Trigger
    async Task PersistTrigger()
    {
        if (NotDto.OldPrice != Price && _dtoRead.OldPrice != Price)
        {
            inProcessFlag = true;
            _dtoPost.FlightCode = NotDto.FlightCode;
            _dtoPost.Price = NotDto.OldPrice;
            tick1 = tick2 = tick3 = priceChange = alertErrorDisplay = ConfigurationSettings.cssHide;

            try
            {
                //Sending Update 
                var response = await Http.PutAsJsonAsync($"{ConfigurationSettings.PriceUpdateUrl}/{NotDto.FlightCode}", _dtoPost);
                tick1 = displayTick;

                //Receiving Response 
                _dtoRead = await response.Content.ReadFromJsonAsync<NotificationMessageDto>();
                tick2 = displayTick;

                //Checking Result
                if (!response.IsSuccessStatusCode)
                {
                    alertErrorDisplay = ConfigurationSettings.cssShow;
                    errorMessage = response.ReasonPhrase;
                }
                //Updating UI
                priceChange = ConfigurationSettings.cssShow;
                tick3 = displayTick;
            }
            catch (NotSupportedException exception)
            {
                errorMessage = exception.Message;
            }

            inProcessFlag = false;
            StateHasChanged();
        }
        else
        {
            alertErrorDisplay = ConfigurationSettings.cssShow;
            errorMessage = samePriceError;
        }

        submitShow = ConfigurationSettings.cssShow;
    }

    }
}
