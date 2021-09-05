using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SpaceXAirlines.Shared.Dto;

namespace SpaceXAirlines.Server.MessageBus
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        // -> Implementing RabbitMq (Placing Message on Queue)

        public MessageBusClient(IConfiguration config)
        {
            _config = config;
        }
        
        public void SendMessage(NotificationMessageDto notificationMessageDto)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "coombes.eastus2.cloudapp.azure.com", 
                UserName = _config["RabbitMqUserName"],
                Password = _config["RabbitMqPassword"],
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                var message = JsonSerializer.Serialize(notificationMessageDto);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "trigger",
                                    routingKey: "",
                                    basicProperties: null,
                                    body: body);
            }


        }
    }
}
