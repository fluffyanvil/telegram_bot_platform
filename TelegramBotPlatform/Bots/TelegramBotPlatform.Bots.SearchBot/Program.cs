using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using TelegramBotPlatform.Bots.SearchBot.GoogleSearch;

namespace TelegramBotPlatform.Bots.SearchBot
{
	class Program
	{
		private static ITelegramBotClient _bot;
		private static string _telegramBotAccessToken = "236618531:AAEm_ErVvUIcftX_av7jSC19iNmKJFqJOgo";
		private static IGoogleSearchEngine _googleSearchEngine;
		static void Main(string[] args)
		{
			_googleSearchEngine = new GoogleSiteSearch();
			
			_bot = new TelegramBotClient(_telegramBotAccessToken);
			Console.WriteLine(_bot.GetMeAsync().Result.FirstName);

			_bot.StartReceiving();

			_bot.OnMessage += BotOnOnMessage;
			_bot.OnInlineQuery += BotOnOnInlineQuery;
			_bot.OnInlineResultChosen += BotOnOnInlineResultChosen;
			
			var command = Console.ReadLine();
		    _bot.SendTextMessageAsync("@gcardrobot", "kirill");
		    Console.ReadLine();
		}

		private static void BotOnOnInlineResultChosen(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
		{
			Console.WriteLine(chosenInlineResultEventArgs.ChosenInlineResult.Query);
		}

		private static async void BotOnOnInlineQuery(object sender, InlineQueryEventArgs inlineQueryEventArgs)
		{
			Console.WriteLine(inlineQueryEventArgs.InlineQuery.Query);
			var results = _googleSearchEngine.Search(inlineQueryEventArgs.InlineQuery.Query);
			if (results != null && results.Any())
			{
				var inlineQuesryResults = results.Select(SearchResultToInlineQueryConverter.Convert).Take(10).ToArray();
				await _bot.AnswerInlineQueryAsync(inlineQueryEventArgs.InlineQuery.Id, inlineQuesryResults, isPersonal:true, cacheTime:300);
			}


		}

		private static void BotOnOnMessage(object sender, MessageEventArgs messageEventArgs)
		{
			Console.WriteLine($"{DateTime.UtcNow}: Received: {messageEventArgs.Message.Text}, from {messageEventArgs.Message.From.Username}");
		}
	}
}
