using Microsoft.Extensions.DependencyInjection;
using Poc.RabbitMQ.Models;
using Poc.RabbitMQ.Services;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc.RabbitMQ
{
    internal class Program : ProgramBase
    {
        static async Task Main()
        {
            long args = 200000;

            using (IConnection connection = GetConnection(rabbitConnection))
            using (IModel channel = connection.CreateModel())
            {
                Parallel.For(0, args, i =>
                {
                    string queue = $"{Guid.NewGuid()}";

                    RabbitMqModel sendRabbitMq = new RabbitMqModel
                    {
                        Queue = queue,
                        RoutingKey = queue,
                        Arguments = new Dictionary<string, object> { { "x-queue-mode", "lazy" } },
                        MessageArray = ToByteArray(JsonValue)
                    };

                    long total = (i * 70);

                    Parallel.For(0, total, q =>
                    {
                        sendService.SendArray(channel, sendRabbitMq);
                        Console.WriteLine($"WRITE-{q}-{queue}");

                        Parallel.For(0, total, r =>
                        {
                            receiveService.ReceiveArray(channel, sendRabbitMq);
                            Console.WriteLine($"READ-{r}-{queue}");
                        });
                    });
                });
            }
        }
    }
}
