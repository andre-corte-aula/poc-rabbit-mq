using Poc.RabbitMQ.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.RabbitMQ.Services
{
    internal class SendService
    {
        public void Send(IModel channel, RabbitMqModel send)
        {
            channel.QueueDeclare(queue: send.Queue,
                durable: send.Durable,
                exclusive: send.Exclusive,
                autoDelete: send.AutoDelete,
                arguments: send.Arguments);

            byte[] body = Encoding.UTF8.GetBytes(send.Message);
            channel.BasicPublish(exchange: send.Exchange, routingKey: send.RoutingKey, basicProperties: null, body: body);
        }

        public void SendArray(IModel channel, RabbitMqModel send)
        {
            channel.QueueDeclare(queue: send.Queue,
                durable: send.Durable,
                exclusive: send.Exclusive,
                autoDelete: send.AutoDelete,
                arguments: send.Arguments);

            channel.BasicPublish(exchange: send.Exchange, routingKey: send.RoutingKey, basicProperties: null, body: send.MessageArray);
        }
    }
}
