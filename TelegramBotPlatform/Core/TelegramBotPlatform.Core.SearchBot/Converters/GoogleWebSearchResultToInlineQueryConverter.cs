using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Customsearch.v1.Data;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.SearchBot.Converters
{
    public class GoogleWebSearchResultToInlineQueryConverter
    {
        public static IEnumerable<InlineQueryResult> Convert(IWebSearchResult searchResult)
        {
            var searchItems = (IEnumerable<Result>)searchResult.Result;

            return searchItems.Select(searchItem => new InlineQueryResultArticle
            {
                Id = Guid.NewGuid().ToString(),
                Description = searchItem.Snippet,
                Url = searchItem.Link,
                ThumbUrl = searchItem.Image?.ThumbnailLink,
                Title = searchItem.Title,
                InputMessageContent = new InputTextMessageContent
                {
                    MessageText = searchItem.Link
                }
            }).Cast<InlineQueryResult>().ToList();
        }
    }
}