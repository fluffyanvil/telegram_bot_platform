using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotPlatform.Core.BingWebSearchApi;
using TelegramBotPlatform.Core.GoogleWebSearch;
using TelegramBotPlatform.Core.SearchBot.Converters;

namespace TelegramBotPlatform.Core.SearchBot
{
    public class SearchBot : ISearchBot
    {
        private static ITelegramBotClient _bot;
        private static IBingWebSearchApi _bingWebSearchApi;
        private static IGoogleWebSearch _googleWebSearch;
        private static IWebSearchAggregator _webSearchAggregator;
        private SearchBot(string telegramBotAccessToken)
        {
            _bot = new TelegramBotClient(telegramBotAccessToken);
            Console.WriteLine(_bot.GetMeAsync().Result.FirstName);
            _bot.OnMessage += BotOnOnMessage;
            _bot.OnInlineQuery += BotOnOnInlineQuery;
            _bot.OnInlineResultChosen += BotOnOnInlineResultChosen;
        }

        public SearchBot(string telegramBotAccessToken, string bingWebSearchApiSubscriptionKey, string googleApiKey, string customSearchCx) : this(telegramBotAccessToken)
        {
            _bingWebSearchApi = new BingWebSearchApi.BingWebSearchApi(bingWebSearchApiSubscriptionKey);
            _googleWebSearch = new GoogleWebSearch.GoogleWebSearch(googleApiKey, customSearchCx);
            _webSearchAggregator = new WebSearchAggregator(_googleWebSearch, _bingWebSearchApi);
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