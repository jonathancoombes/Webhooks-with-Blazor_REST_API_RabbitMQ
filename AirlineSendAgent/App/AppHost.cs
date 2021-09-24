using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineSendAgent.Client;
using AirlineSendAgent.Data;
using AirlineSendAgent.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TravelAgentWeb.Dtos;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AirlineSendAgent.App
{
    public class AppHost : IAppHost
    {
        private readonly SendAgentDbContext _context;
        private readonly IWebhookClient _webHookClient;
        private readonly IConfiguration _config;
        string _url = "amqps://pwyalywp:0r1MyUhVkp9g2Ix7RRS0w7XXSos4eVKm@turkey.rmq.cloudamqp.com/pwyalywp";

        public AppHost(SendAgentDbContext context, IWebhookClient webhookClient, IConfiguration config)
        {
            _context = context;
            _webHookClient = webhookClient;
            _config = config;
        }

        // ! --> Create Connection
        
        public void Run()
        {
            var factory = new ConnectionFactory()
            {
                //HostName = "localhost",                 
                Uri = new Uri(_url)

                // Port = 5672
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                
                //Creating and binding queue to channel
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "trigger", routingKey: "");

                //Creating consumer onto the channel
                var consumer = new EventingBasicConsumer(channel);
                Console.WriteLine("Listening on the message bus");

                // Registering consumer.received (message) event handler to event
                // -> to be awaited still
                consumer.Received += async (ModuleHandle, EventArgs) =>
                {
                    Console.WriteLine("Event is triggered");

                    var body = EventArgs.Body.ToArray();
                    var notificationMessage = Encoding.UTF8.GetString(body);
                    var message = JsonSerializer.Deserialize<NotificationMessageDto>(notificationMessage);

                    
                    var webhookToSend = new FlightDetailChangePayloadDto()
                    {
                        WebhookType = message.WebhookType,
                        WebhookURI = string.Empty,
                        Secret = string.Empty,
                        Publisher = string.Empty,
                        OldPrice = message.OldPrice,
                        NewPrice = message.NewPrice,
                        FlightCode = message.FlightCode

                    };
                    //Check logic!
                    foreach (var subs in _context.WebhookSubscriptions
                        .Where(s => s.WebhookType.Equals(message.WebhookType)))
                    {
                        webhookToSend.WebhookURI = subs.WebhookUri;
                        webhookToSend.Secret = subs.Secret;
                        webhookToSend.Publisher = subs.WebhookPublisher;

                        await _webHookClient.SendWebhookNotification(webhookToSend);
                    }
                    
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                //Keeping Console open
                Console.ReadLine();
            };
            
        }
    }
}
