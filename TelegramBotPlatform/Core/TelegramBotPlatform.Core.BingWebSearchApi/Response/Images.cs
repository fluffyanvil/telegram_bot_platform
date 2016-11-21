using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    public class Images
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("readLink")]
        public string ReadLink { get; set; }
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }
        [JsonProperty("isFamilyFriendly")]
        public bool IsFamilyFriendly { get; set; }
        [JsonProperty("value")]
        public Image[] Value { get; set; }
    }
}