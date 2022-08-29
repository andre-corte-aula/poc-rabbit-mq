using Poc.RabbitMQ.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.RabbitMQ.Services
{
    internal class ReceiveService
    {
        public string Receive(IModel channel, RabbitMqModel send)
        {
            channel.QueueDeclare(queue: send.Queue,
              durable: send.Durable,
              exclusive: send.Exclusive,
              autoDelete: send.AutoDelete,
              arguments: send.Arguments);

            BasicGetResult basicGetResult = channel.BasicGet(send.Queue, send.AutoAck);
            return basicGetResult == null ? null : Encoding.UTF8.GetString(basicGetResult.Body.ToArray());
        }

        public byte[] ReceiveArray(IModel channel, RabbitMqModel send)
        {
            channel.QueueDeclare(queue: send.Queue,
              durable: send.Durable,
              exclusive: send.Exclusive,
              autoDelete: send.AutoDelete,
              arguments: send.Arguments);

            BasicGetResult basicGetResult = channel.BasicGet(send.Queue, send.AutoAck);
            return basicGetResult == null ? null : basicGetResult.Body.ToArray();
        }
    }
}
