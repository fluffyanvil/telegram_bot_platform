using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotPlatform.Core.BingWebSearchApi;

namespace TelegramBotPlatform.Bots.SearchBot
{
	class Program
	{
	    private const string TelegramBotAccessToken = "236618531:AAEm_ErVvUIcftX_av7jSC19iNmKJFqJOgo";
	    private const string BingWebsearchApiSubscriptionKey = "6508d56f058048219b8a35a68ff8596b";
	    private static ISearchBot _bot;
		static void Main(string[] args)
		{
            _bot = new SearchBot(TelegramBotAccessToken, BingWebsearchApiSubscriptionKey);
            _bot.Start();
            Console.WriteLine("Press Enter to exit...");
			Console.ReadLine();
		}
	}
}
