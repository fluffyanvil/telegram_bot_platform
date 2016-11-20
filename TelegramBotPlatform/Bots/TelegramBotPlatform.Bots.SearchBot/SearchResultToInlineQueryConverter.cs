using System;
using Google.Apis.Customsearch.v1.Data;
using HtmlAgilityPack;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;

namespace TelegramBotPlatform.Bots.SearchBot
{
	public class SearchResultToInlineQueryConverter
	{
		public static InlineQueryResult Convert(Result searchResult)
		{
			var result = new InlineQueryResultArticle
			{
				Id = Guid.NewGuid().ToString(),
				Url = searchResult.Link,
				ThumbUrl = searchResult.Image?.ContextLink,
				Description = searchResult.Title,
				Title = searchResult.Title,
				InputMessageContent = new InputTextMessageContent()
				{
					MessageText = searchResult.HtmlFormattedUrl
				}
			};
			return result;
		}

		public static InlineQueryResult Convert(HtmlNode searchResult)
		{
			var result = new InlineQueryResultArticle
			{
				Id = Guid.NewGuid().ToString(),
				InputMessageContent = new InputTextMessageContent()
				{
					MessageText = searchResult.InnerHtml,
					ParseMode = ParseMode.Markdown
				}
			};
			return result;
		}
	}
}