using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.SearchBot
{
    public class SearchBot : ISearchBot
    {
        private static ITelegramBotClient _bot;
        private static IWebSearchAggregator _webSearchAggregator;
        private SearchBot(string telegramBotAccessToken)
        {
            _bot = new TelegramBotClient(telegramBotAccessToken);
            Console.WriteLine(_bot.GetMeAsync().Result.FirstName);
            _bot.OnMessage += BotOnOnMessage;
            _bot.OnInlineQuery += BotOnOnInlineQuery;
            _bot.OnInlineResultChosen += BotOnOnInlineResultChosen;
        }

        public SearchBot(string telegramBotAccessToken, params IWebSearchEngine[] engines) : this(telegramBotAccessToken)
        {
            _webSearchAggregator = new WebSearchAggregator(engines);
        }

        public void Start()
        {
            _bot.StartReceiving();
        }

        public void Stop()
        {
            _bot.StopReceiving();
        }

        private static void BotOnOnInlineResultChosen(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine(chosenInlineResultEventArgs.ChosenInlineResult.Query);
        }

        private static async void BotOnOnInlineQuery(object sender, InlineQueryEventArgs inlineQueryEventArgs)
        {
            Console.WriteLine($"{DateTime.Now} | {inlineQueryEventArgs.InlineQuery.From.Username}: {inlineQueryEventArgs.InlineQuery.Query}");
            var queryString = inlineQueryEventArgs.InlineQuery.Query;
            var result = await _webSearchAggregator.Search(queryString);
            if (result == null) return;
            try
            {
                await _bot.AnswerInlineQueryAsync(inlineQueryEventArgs.InlineQuery.Id, result.ToArray(), isPersonal: true, cacheTime: 300);            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now}: {ex}");
            }

        }

        private static void BotOnOnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine($"{DateTime.UtcNow} | {messageEventArgs.Message.From.Username}: {messageEventArgs.Message.Text}");
        }
    }
}