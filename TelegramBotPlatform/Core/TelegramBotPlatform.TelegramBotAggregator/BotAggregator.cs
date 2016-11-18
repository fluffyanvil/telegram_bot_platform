using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotPlatform.Core.TelegramBotAggregator.Config;
using TelegramBotPlatform.Core.TelegramBotAggregator.EventArgs;

namespace TelegramBotPlatform.Core.TelegramBotAggregator
{
    public class BotAggregator : IBotAggregator
    {
        public BotAggregator(BotAggregatorConfig config)
        {
            if (!config.BotAccessTokens.Any())
                return;
            Bots = config.BotAccessTokens.Select(token => new TelegramBotClient(token));
        }

        public void Start()
        {
            foreach (var telegramBotClient in Bots)
            {
                telegramBotClient.StartReceiving();
                telegramBotClient.OnMessage += TelegramBotClientOnOnMessage;
            }
        }

        public void Stop()
        {
            foreach (var telegramBotClient in Bots)
            {
                telegramBotClient.StopReceiving();
                telegramBotClient.OnMessage -= TelegramBotClientOnOnMessage;
            }
        }

        private void TelegramBotClientOnOnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            OnMessage?.Invoke(this, new BotAggregatorOnMessageEventArgs((ITelegramBotClient)sender, messageEventArgs.Message));
        }

        public IEnumerable<ITelegramBotClient> Bots { get; private set; }
        public event EventHandler OnCallbackQuery;
        public event EventHandler OnInlineQuery;
        public event EventHandler OnInlineResultChosen;
        public event EventHandler<BotAggregatorOnMessageEventArgs> OnMessage;
        public event EventHandler OnMessageEdited;
        public event EventHandler OnReceiveError;
        public event EventHandler OnReceiveGeneralError;
        public event EventHandler OnUpdate;
    }
}