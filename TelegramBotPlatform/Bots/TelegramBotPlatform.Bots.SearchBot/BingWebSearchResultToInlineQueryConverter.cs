using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using TelegramBotPlatform.Core.BingWebSearchApi.Response;

namespace TelegramBotPlatform.Bots.SearchBot
{
	public class BingWebSearchResultToInlineQueryConverter
	{
		public static IEnumerable<InlineQueryResult> Convert(BingWebSearchResult bingWebSearchResult)
		{
			var result = new List<InlineQueryResult>();
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

		private static IEnumerable<InlineQueryResultPhoto> ConvertImagesToInlineQueryResultPhoto(Images images)
		{
			if (images == null) return new List<InlineQueryResultPhoto>();
			var imagesValues = images.Value;
			var result = imagesValues.Select(i => new InlineQueryResultPhoto()
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