using System;
using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    public class WebPage
    {
        [JsonProperty("id")]
        public string Id { get; set; } 
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("displayUrl")]
        public string DisplayUrl { get; set; }
        [JsonProperty("snippet")]
        public string Snippet { get; set; }
    }
}