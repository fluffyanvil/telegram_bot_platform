using System;
using TelegramBotPlatform.Bots.SearchBot.Properties;
using TelegramBotPlatform.Core.BingWebSearchApi;
using TelegramBotPlatform.Core.SearchBot;

namespace TelegramBotPlatform.Bots.SearchBot
{
	class Program
	{
	    private const string GoogleApiKey = "AIzaSyCfLsEYZ17kPnKGZwN5p90MDQfAuo65AlQ";
	    private const string GoogleCustomSearchEngineCx = "010413868545992504530:xysqnrglcne";
	    private static ISearchBot _bot;
		static void Main(string[] args)
		{
		    var settings = Settings.Default;
		    var telegramBotAccessToken = settings.TelegramBotAccessToken;
            var bingWebSearchApi = new BingWebSearchApi(settings.BingWebSearchApiSubscriptionKey);
            _bot = new Core.SearchBot.SearchBot(telegramBotAccessToken, bingWebSearchApi);
            _bot.Start();
            Console.WriteLine("Press Enter to exit...");
			Console.ReadLine();
		}
	}
}
