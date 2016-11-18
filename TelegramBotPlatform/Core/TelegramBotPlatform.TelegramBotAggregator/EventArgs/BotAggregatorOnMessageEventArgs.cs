using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotPlatform.Core.TelegramBotAggregator.EventArgs
{
    public class BotAggregatorOnMessageEventArgs : System.EventArgs
    {
        public ITelegramBotClient Bot { get; }
        public Message Message { get; }

        public BotAggregatorOnMessageEventArgs(ITelegramBotClient bot, Message message)
        {
            Bot = bot;
            Message = message;
        }
    }
}