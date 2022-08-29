using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.RabbitMQ.Models
{
    internal class RabbitMqModel
    {
        public RabbitMqModel()
        {
            Arguments = null;
            Durable = false;
            Exclusive = false;
            AutoDelete = false;
            AutoAck = true;
            Exchange = string.Empty;
            RoutingKey = string.Empty;
        }

        public string Queue { get; set; }
        public string RoutingKey { get; set; }
        public string Exchange { get; set; }
        public string Message { get; set; }
        public byte[] MessageArray { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public bool AutoAck { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }
}
