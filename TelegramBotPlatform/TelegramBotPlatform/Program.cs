using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TelegramBotPlatform.Core.Constants;
using TelegramBotPlatform.Core.TelegramBotAggregator;
using TelegramBotPlatform.Core.TelegramBotAggregator.Config;
using TelegramBotPlatform.Core.TelegramBotAggregator.EventArgs;

namespace TelegramBotPlatform.Console
{
    class Program
    {
        private static IModel _channel;
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory {Uri = RabbitMqConstants.BrokerUrl.Replace("amqp://", "amqps://")};

            var aggegator = new BotAggregator(BotAggregatorConfig.SampleBotAggregatorConfig);
            aggegator.Start();
            aggegator.OnMessage += AggegatorOnOnMessage;

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: RabbitMqConstants.BotAggregatorTester1Queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                System.Console.WriteLine($"Received {message} in {ea.RoutingKey}");
            };
            _channel.BasicConsume(queue: RabbitMqConstants.TestQueueName,
                                 noAck: true,
                                 consumer: consumer);
        }

        private static void AggegatorOnOnMessage(object sender, BotAggregatorOnMessageEventArgs botAggregatorOnMessageEventArgs)
        {
            var firstName = botAggregatorOnMessageEventArgs.Bot.GetMeAsync().Result.FirstName;
            System.Console.WriteLine($"Bot: {firstName}, Received: {botAggregatorOnMessageEventArgs.Message.Text}");;
            var body = Encoding.UTF8.GetBytes(botAggregatorOnMessageEventArgs.Message.Text);

            _channel.BasicPublish(exchange: "",
                                 routingKey: firstName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
