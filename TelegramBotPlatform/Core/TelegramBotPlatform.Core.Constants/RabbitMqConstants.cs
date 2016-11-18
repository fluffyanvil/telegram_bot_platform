namespace TelegramBotPlatform.Core.Constants
{
    public static class RabbitMqConstants
    {
        public static string Host = "spotted-monkey.rmq.cloudamqp.com";
        public static string Password = "UdROpPV86fsNa29M7yD1tdE-RmVKU7By";
        public static string UserVhost = "etlplrrc";
        public static string BrokerUrl = "amqp://etlplrrc:UdROpPV86fsNa29M7yD1tdE-RmVKU7By@spotted-monkey.rmq.cloudamqp.com/etlplrrc";
        public static string TestQueueName = "testQueue";
        public static string BotAggregatorTester1Queue = "BotAggregatorTester1";
        public static string BotAggregatorTester2Queue = "BotAggregatorTester2";
    }
}