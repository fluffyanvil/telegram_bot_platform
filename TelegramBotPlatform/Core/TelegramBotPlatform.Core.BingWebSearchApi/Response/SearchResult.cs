using Newtonsoft.Json;
using TelegramBotPlatform.Core.Common.Enums;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    [JsonObject]
    public class SearchResult
    {
        [JsonProperty("queryContext")]
        public QueryContext QueryContext { get; set; }
		[JsonProperty("webPages")]
		public WebPages WebPages { get; set; }
		[JsonProperty("images")]
		public Images Images { get; set; }
		[JsonProperty("videos")]
		public Videos Videos { get; set; }
    }
}