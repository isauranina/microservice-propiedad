using Application.Interfaces.IServices.Rabbit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Rabbit
{
    public  class RabbitConsumerService : IRabbitConsumerService
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demostracion", ExchangeType.Fanout);
            channel.QueueDeclare(queue: "",
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

            };
            channel.BasicConsume("demostracion", true, consumer);
        }
    }
}
