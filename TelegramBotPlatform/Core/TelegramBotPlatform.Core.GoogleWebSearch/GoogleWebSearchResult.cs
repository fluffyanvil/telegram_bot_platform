using System.Collections.Generic;
using Google.Apis.Customsearch.v1.Data;
using TelegramBotPlatform.Core.Common.Enums;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.GoogleWebSearch
{
    public class GoogleWebSearchResult : IWebSearchResult
    {
        public IEnumerable<Result> Results { get; set; }
        public SearchEngine SearchEngine => SearchEngine.Google;
        public object Result => Results;
    }
}