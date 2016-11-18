using System;
using System.Collections.Generic;
using Telegram.Bot;
using TelegramBotPlatform.Core.TelegramBotAggregator.EventArgs;

namespace TelegramBotPlatform.Core.TelegramBotAggregator
{
    public interface IBotAggregator
    {
        IEnumerable<ITelegramBotClient> Bots { get; }
        void Start();
        void Stop();
        event EventHandler OnCallbackQuery;
        event EventHandler OnInlineQuery;
        event EventHandler OnInlineResultChosen;
        event EventHandler<BotAggregatorOnMessageEventArgs> OnMessage;
        event EventHandler OnMessageEdited;
        event EventHandler OnReceiveError;
        event EventHandler OnReceiveGeneralError;
        event EventHandler OnUpdate;
    }
}