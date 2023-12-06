using Application.Interfaces.IServices.Rabbit;
using Domain.Models.Rabbit;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Rabbit
{
    public class AmqpService:IAmqpService
    {
        #region "Atributos"
        // private AmqpInfo _amqpInfo;
        //private RabbitSetting _rabbitSetting;
        private ConnectionFactory _connectionFactory;
        // private const string QueueName = "DemoQueue";
       // private ColaSetting _colaSetting;
        #endregion

        #region "Constructor"
        // public AmqpService(IOptions<AmqpInfo> ampOptionsSnapshot)
        public AmqpService()
        {
            //_rabbitSetting = appSettings.RabbitSetting;
            
            var connectionString = new Uri("amqps://farhdenj:BilLhsNpcQHME1p2ItwtM5sZImZaqmDC@shrimp.rmq.cloudamqp.com/farhdenj");
             this._connectionFactory = new ConnectionFactory() {
                DispatchConsumersAsync = true,
                Uri = connectionString 
            };
            
        }
        #endregion
        #region "Propiedades"   

        #endregion
        #region "Metodos"
        public bool Publish(object objeto, string cola)
        {
            try
            {

                using (var connection = this._connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                       
                     
                        channel.ExchangeDeclare(
                            exchange: "demostracion",
                            type: ExchangeType.Fanout,
                            durable: true, 
                            autoDelete:false,
                            arguments:null
                            );
                        //channel.QueueDeclare(queue: "",
                        //                     durable: true,
                        //                     exclusive: false,
                        //                     autoDelete: false,
                        //                     arguments: null);

                          // channel.QueueBind("", "demostracion", "", null);
                       var message = Newtonsoft.Json.JsonConvert.SerializeObject(objeto);
                         var body = Encoding.UTF8.GetBytes(message);
//                        string strCadena As String
//strCadena = "La siguiente "
//strCadena = strCadena & """"
//strCadena = strCadena & "palabra"
//strCadena = strCadena & """"
//strCadena = strCadena & " está entre comillas dobles"

                        //var body = Encoding.UTF8.GetBytes("'"+"isaura"+"'");
                      

                        channel.BasicPublish(exchange: "demostracion",
                                             routingKey: "",                                             
                                            basicProperties:null,
                                             body: body                                            
                                             
                                             );

                        channel.Close();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        #endregion
    }
}
