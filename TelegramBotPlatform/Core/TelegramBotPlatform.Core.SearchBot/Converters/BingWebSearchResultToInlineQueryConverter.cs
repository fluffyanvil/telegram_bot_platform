using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using TelegramBotPlatform.Core.BingWebSearchApi.Response;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.SearchBot.Converters
{
	public class BingWebSearchResultToInlineQueryConverter
	{
		public static IEnumerable<InlineQueryResult> Convert(IWebSearchResult searchResult)
		{
			var result = new List<InlineQueryResult>();
		    var bingWebSearchResult = (SearchResult)searchResult.Result;
            result.AddRange(ConvertWebPagesToInlineQueryResultArticle(bingWebSearchResult.WebPages));
			result.AddRange(ConvertImagesToInlineQueryResultPhoto(bingWebSearchResult.Images));
			result.AddRange(ConvertVideosToInlineQueryResultVideo(bingWebSearchResult.Videos));
			return result;
		}

		private static IEnumerable<InlineQueryResultArticle> ConvertWebPagesToInlineQueryResultArticle(WebPages pages)
		{
			if (pages == null) return new List<InlineQueryResultArticle>();
			var pageValues = pages.Value;
			var result = pageValues.Select(p => new InlineQueryResultArticle
			{
				Id = Guid.NewGuid().ToString(),
				Title = p.Name,
				Description = p.Snippet,
				Url = p.Url,
				InputMessageContent = new InputTextMessageContent
				{
					MessageText = p.Url
				}
			});
			return result.ToArray();
		}

		private static IEnumerable<InlineQueryResultArticle> ConvertImagesToInlineQueryResultPhoto(Images images)
		{
			if (images == null) return new List<InlineQueryResultArticle>();
			var imagesValues = images.Value;
			var result = imagesValues.Select(i => new InlineQueryResultArticle()
			{
				Id = Guid.NewGuid().ToString(),
				Title = i.Name,
				Url = i.ContentUrl,
				Description = i.Name,
				ThumbUrl = i.ThumbnailUrl,
				InputMessageContent = new InputTextMessageContent
				{
					MessageText = i.ContentUrl
				}
			});
			return result.ToArray();
		}

		private static IEnumerable<InlineQueryResultVideo> ConvertVideosToInlineQueryResultVideo(Videos videos)
		{
			if (videos == null) return new List<InlineQueryResultVideo>();
			var videosValues = videos.Value;
			var result = videosValues.Select(i => new InlineQueryResultVideo()
			{
				Id = Guid.NewGuid().ToString(),
				Title = i.Name,
				Url = i.ContentUrl,
				Description = i.Name,
				Caption = i.Name,
				ThumbUrl = i.ThumbnailUrl,
				InputMessageContent = new InputTextMessageContent
				{
					MessageText = i.ContentUrl
				},
				MimeType = "video/mp4"
			});
			return result.ToArray();
		}
	}
}