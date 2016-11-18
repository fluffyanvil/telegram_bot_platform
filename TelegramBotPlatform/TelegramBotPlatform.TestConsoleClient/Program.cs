using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TelegramBotPlatform.Core.Constants;

namespace TelegramBotPlatform.TestConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory {Uri = RabbitMqConstants.BrokerUrl.Replace("amqp://", "amqps://")};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: RabbitMqConstants.BotAggregatorTester1Queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };
                    channel.BasicConsume(queue: RabbitMqConstants.BotAggregatorTester1Queue,
                                         noAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
