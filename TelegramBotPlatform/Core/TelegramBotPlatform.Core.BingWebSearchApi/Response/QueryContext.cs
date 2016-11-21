using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    public class QueryContext
    {
        [JsonProperty("originalQuery")]
        public string OriginalQuery { get; set; }

        [JsonProperty("adultIntent")]
        public bool AdultIntent { get; set; }
    }
}