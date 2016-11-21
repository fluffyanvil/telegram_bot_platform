using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.BingWebSearchApi.Response
{
    public class Video
    {
        //       "webSearchUrlPingSuffix": "DevEx,5430.1",
        //       "thumbnailUrl": "https://tse2.mm.bing.net/th?id=WN.qLvK%2bJin%2b21addnQ0lsxUw&pid=Api",
        //       "datePublished": "2013-05-14T18:35:52",
        //       "publisher": [
        //           {
        //               "name": "YouTube"
        //           }
        //       ],
        //       "contentUrl": "https://www.youtube.com/watch?v=cPy0nWYYCFg",
        //       "hostPageUrl": "https://www.youtube.com/watch?v=cPy0nWYYCFg",
        //       "hostPageUrlPingSuffix": "DevEx,5429.1",
        //       "encodingFormat": "mp4",
        //       "hostPageDisplayUrl": "https://www.youtube.com/watch?v=cPy0nWYYCFg",
        //       "width": 1280,
        //       "height": 720,
        //       "duration": "PT13M42S",
        //       "motionThumbnailUrl": "https://tse2.mm.bing.net/th?id=OM.DnxwxcadjE1OIw&pid=Api",
        //       "embedHtml": "<iframe width=\"1280\" height=\"720\" src=\"http://www.youtube.com/embed/cPy0nWYYCFg?autoplay=1\" frameborder=\"0\" allowfullscreen></iframe>",
        //       "allowHttpsEmbed": true,
        //       "viewCount": 80292,
        //       "thumbnail": {
        //           "width": 300,
        //           "height": 168
        //       }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }
}