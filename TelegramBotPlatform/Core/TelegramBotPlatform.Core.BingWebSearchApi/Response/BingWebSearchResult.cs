using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    [JsonObject]
    public class BingWebSearchResult
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