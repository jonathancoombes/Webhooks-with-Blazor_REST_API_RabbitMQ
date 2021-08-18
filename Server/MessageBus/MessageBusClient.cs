using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SpaceXAirlines.Shared.Dto;

namespace SpaceXAirlines.Server.MessageBus
{
    public class MessageBusClient : IMessageBusClient
    {
        // -> Implementing RabbitMq (Placing Message on Queue)
        
        public void SendMessage(NotificationMessageDto notificationMessageDto)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };

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
