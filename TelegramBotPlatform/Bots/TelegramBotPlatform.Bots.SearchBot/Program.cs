using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBotPlatform.Core.BingWebSearchApi;

namespace TelegramBotPlatform.Bots.SearchBot
{
	class Program
	{
		private static ITelegramBotClient _bot;
		private static string _telegramBotAccessToken = "236618531:AAEm_ErVvUIcftX_av7jSC19iNmKJFqJOgo";
		private static string _bingWebsearchApiSubscriptionKey = "6508d56f058048219b8a35a68ff8596b";
		public static IBingWebSearchApi BingWebSearchApi;
		static void Main(string[] args)
		{
			BingWebSearchApi = new BingWebSearchApi(_bingWebsearchApiSubscriptionKey);
			_bot = new TelegramBotClient(_telegramBotAccessToken);
			Console.WriteLine(_bot.GetMeAsync().Result.FirstName);
			_bot.StartReceiving();
			_bot.OnMessage += BotOnOnMessage;
			_bot.OnInlineQuery += BotOnOnInlineQuery;
			_bot.OnInlineResultChosen += BotOnOnInlineResultChosen;

			Console.WriteLine("Press Enter to exit...");
			Console.ReadLine();
		}

		private static void BotOnOnInlineResultChosen(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
		{
			Console.WriteLine(chosenInlineResultEventArgs.ChosenInlineResult.Query);
		}

		private static async void BotOnOnInlineQuery(object sender, InlineQueryEventArgs inlineQueryEventArgs)
		{
			Console.WriteLine($"{DateTime.Now} | {inlineQueryEventArgs.InlineQuery.From.Username}: {inlineQueryEventArgs.InlineQuery.Query}");
			var result = await BingWebSearchApi.SearchAsync(inlineQueryEventArgs.InlineQuery.Query);
			if (result == null) return;
			var inlineQuesryResults = BingWebSearchResultToInlineQueryConverter.Convert(result).ToArray();
			try
			{
				await _bot.AnswerInlineQueryAsync(inlineQueryEventArgs.InlineQuery.Id, inlineQuesryResults, isPersonal: true, cacheTime: 300);
			}
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
