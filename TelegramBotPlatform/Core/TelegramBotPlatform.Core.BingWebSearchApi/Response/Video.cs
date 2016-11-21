using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    public class Video
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
		[JsonProperty("contentUrl")]
		public string ContentUrl { get; set; }
		[JsonProperty("hostPageUrl")]
		public string HostPageUrl { get; set; }
		[JsonProperty("hostPageDisplayUrl")]
		public string HostPageDisplayUrl { get; set; }
		[JsonProperty("motionThumbnailUrl")]
		public string MotionThumbnailUrl { get; set; }
	}
}