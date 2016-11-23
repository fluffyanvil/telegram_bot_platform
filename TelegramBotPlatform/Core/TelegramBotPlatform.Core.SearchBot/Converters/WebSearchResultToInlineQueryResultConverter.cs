using System;
using System.Collections.Generic;
using Telegram.Bot.Types.InlineQueryResults;
using TelegramBotPlatform.Core.Common.Enums;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.SearchBot.Converters
{
    public class WebSearchResultToInlineQueryResultConverter
    {
        public static IEnumerable<InlineQueryResult> Convert(IWebSearchResult webSearchResult)
        {
            switch (webSearchResult.SearchEngine)
            {
                case SearchEngine.Google:
                    return GoogleWebSearchResultToInlineQueryConverter.Convert(webSearchResult);
                case SearchEngine.Bing:
                    return BingWebSearchResultToInlineQueryConverter.Convert(webSearchResult);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}