using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    [JsonObject]
    public class BingWebSearchResponse
    {
        [JsonProperty("queryContext")]
        public QueryContext QueryContext { get; set; }


    }
}