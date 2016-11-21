using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    public class WebPages
    {
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; } 
        [JsonProperty("totalEstimatedMatches")]
        public double TotalEstimatedMatches { get; set; }
        [JsonProperty("value")]
        public WebPage[] Value { get; set; }
    }
}