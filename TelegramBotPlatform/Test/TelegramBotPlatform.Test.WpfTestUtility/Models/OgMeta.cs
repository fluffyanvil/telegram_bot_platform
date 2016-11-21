namespace TelegramBotPlatform.Test.WpfTestUtility.Models
{
    public class OgMeta
    {
        public OgMeta()
        {
            Video = new OgVideo();
        }

        public string AppId { set; get; }
        public string SiteName { set; get; }
        public string Locale { set; get; }
        public string Type { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }
        public string Image { set; get; }
        public string Audio { set; get; }
        public OgVideo Video { set; get; }
    }
}