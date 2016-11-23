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
        public static IBingWebSearchApi BingWebSearchApi;
        public static IGoogleWebSearch GoogleWebSearch;
        private SearchBot(string telegramBotAccessToken)
        {
            _bot = new TelegramBotClient(telegramBotAccessToken);
            Console.WriteLine(_bot.GetMeAsync().Result.FirstName);
            _bot.OnMessage += BotOnOnMessage;
            _bot.OnInlineQuery += BotOnOnInlineQuery;
            _bot.OnInlineResultChosen += BotOnOnInlineResultChosen;
        }

        public SearchBot(string telegramBotAccessToken, string bingWebSearchApiSubscriptionKey) : this(telegramBotAccessToken)
        {
            BingWebSearchApi = new BingWebSearchApi.BingWebSearchApi(bingWebSearchApiSubscriptionKey);
        }

        public SearchBot(string telegramBotAccessToken, string googleApiKey, string customSearchCx) : this(telegramBotAccessToken)
        {
            GoogleWebSearch = new GoogleWebSearch.GoogleWebSearch(googleApiKey, customSearchCx);
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
            var result = BingWebSearchApi != null ? await BingWebSearchApi.SearchAsync(queryString) : await GoogleWebSearch.Search(queryString);
            if (result == null) return;
            var inlineQuesryResults = WebSearchResultToInlineQueryResultConverter.Convert(result).ToArray();
            try
            {
                await _bot.AnswerInlineQueryAsync(inlineQueryEventArgs.InlineQuery.Id, inlineQuesryResults, isPersonal: true, cacheTime: 300);            }
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