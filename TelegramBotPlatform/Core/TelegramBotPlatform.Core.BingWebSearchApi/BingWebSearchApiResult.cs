using TelegramBotPlatform.Core.BingWebSearchApi.Response;
using TelegramBotPlatform.Core.Common.Enums;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.BingWebSearchApi
{
    public class BingWebSearchApiResult : IWebSearchResult
    {
        public SearchResult SearchResult { get; set; }
        public SearchEngine SearchEngine => SearchEngine.Bing;
        public object Result => SearchResult;
    }
}