using Microsoft.Extensions.DependencyInjection;
using Poc.RabbitMQ.Models;
using Poc.RabbitMQ.Services;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.RabbitMQ
{
    internal class ProgramBase
    {
        protected static readonly ReceiveService receiveService;
        protected static readonly SendService sendService;

        static ProgramBase()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ReceiveService>();
            serviceCollection.AddSingleton<SendService>();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            receiveService = serviceProvider.GetService<ReceiveService>();
            sendService = serviceProvider.GetService<SendService>();
        }

        protected static RabbitConnectionViewModel rabbitConnection => new RabbitConnectionViewModel
        {
            ApplicationName = "Poc.RabbitMQ",
            Version = "1.0"
        };

        static string connectionString = "amqps://ayudsetg:SxmwSs7c954Ju85OU_ZcM1cKJxx7LJ_y@jackal.rmq.cloudamqp.com/ayudsetg";

        protected static IConnection GetConnection(RabbitConnectionViewModel rabbitConnection) => new ConnectionFactory()
        {
            Uri = new Uri(connectionString),
            ClientProperties = new Dictionary<string, object>
            {
                { "eventflow-version", typeof(Program).Assembly.GetName().Version.ToString() },
                { "machine-name", Environment.MachineName },
                { "application-name", rabbitConnection.ApplicationName },
                { "version", rabbitConnection.Version },
                { "connection_name", Environment.MachineName },
            },
        }.CreateConnection();

        protected static byte[] ToByteArray(string value)
        {
            char[] charArr = value.ToCharArray();
            byte[] bytes = new byte[charArr.Length];
            for (int i = 0; i < charArr.Length; i++)
            {
                byte current = Convert.ToByte(charArr[i]);
                bytes[i] = current;
            }

            return bytes;
        }

        protected static string JsonValue => @"'{\'$schema\':\'https://json-schema.org/draft/2020-12/schema\',\'$id\':\'https://example.com/product.schema.json\',\'title\':\'Product\',\'description\':\'AproductfromAcme'scatalog\',\'type\':\'object\',\'properties\':{\'productId\':{\'description\':\'Theuniqueidentifierforaproduct\',\'type\':\'integer\'},\'productName\':{\'description\':\'Nameoftheproduct\',\'type\':\'string\'},\'price\':{\'description\':\'Thepriceoftheproduct\',\'type\':\'number\',\'exclusiveMinimum\':0},\'tags\':{\'description\':\'Tagsfortheproduct\',\'type\':\'array\',\'items\':{\'type\':\'string\'},\'minItems\':1,\'uniqueItems\':true},\'dimensions\':{\'type\':\'object\',\'properties\':{\'length\':{\'type\':\'number\'},\'width\':{\'type\':\'number\'},\'height\':{\'type\':\'number\'}},\'required\':[\'length\',\'width\',\'height\']},\'warehouseLocation\':{\'description\':\'Coordinatesofthewarehousewheretheproductislocated.\',\'$ref\':\'https://example.com/geographical-location.schema.json\'}},\'required\':[\'productId\',\'productName\',\'price\']}'";
    }
}
