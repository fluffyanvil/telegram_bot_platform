using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types.InlineQueryResults;
using TelegramBotPlatform.Core.BingWebSearchApi;
using TelegramBotPlatform.Core.Common.Interfaces;
using TelegramBotPlatform.Core.GoogleWebSearch;
using TelegramBotPlatform.Core.SearchBot.Converters;

namespace TelegramBotPlatform.Core.SearchBot
{
    public class WebSearchAggregator : IWebSearchAggregator
    {
        private IGoogleWebSearch _googleWebSearch;
        private IBingWebSearchApi _bingWebSearchApi;
        public WebSearchAggregator(IGoogleWebSearch googleWebSearch, IBingWebSearchApi bingWebSearchApi)
        {
            _googleWebSearch = googleWebSearch;
            _bingWebSearchApi = bingWebSearchApi;
        }

        public async Task<IEnumerable<InlineQueryResult>> Search(string query, bool searchImages = false)
        {
            var inlineQueryResults = new List<InlineQueryResult>();
            if (string.IsNullOrEmpty(query)) return inlineQueryResults;
            var googleResults = await _googleWebSearch.SearchAsync(query, searchImages);
            var bingResults = await _bingWebSearchApi.SearchAsync(query);
            inlineQueryResults.AddRange(WebSearchResultToInlineQueryResultConverter.Convert(googleResults));
            inlineQueryResults.AddRange(WebSearchResultToInlineQueryResultConverter.Convert(bingResults));
            return inlineQueryResults;
        }
    }
}