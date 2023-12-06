using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Domain.Models.Rabbit;

namespace Application.Services.Rabbit
{
    public class RabbitService
    {
      //  private RabbitSetting _rabbitSetting;
       // private ConnectionFactory _connectionFactory;
        //public async Task<bool> Publish(object objeto,string nombre_cola)
        //{
        //    try {
        //        using (var connection = this._connectionFactory.CreateConnection())
        //        {
        //            using (var channel = connection.CreateModel())
        //            {
        //                channel.QueueDeclare(queue: nombre_cola,
        //                                    durable: true,
        //                                    exclusive: false,
        //                                    autoDelete: false,
        //                                    arguments: null);

        //                var message = Newtonsoft.Json.JsonConvert.SerializeObject(objeto);
        //                var body = Encoding.UTF8.GetBytes(message);

        //                IBasicProperties props = channel.CreateBasicProperties();
        //                props.ContentType = "text/plain";
        //                props.DeliveryMode = 2;

        //                channel.BasicPublish(exchange: "demostracion",
        //                                     routingKey: nombre_cola,
        //                                    // basicProperties: props,
        //                                     body: body);

        //                channel.Close();

        //            }
        //            connection.Close();
        //            return true;
        //        }
               
        //    }
        //    catch (Exception ex) { return false;  }
        //}
    }    
}
