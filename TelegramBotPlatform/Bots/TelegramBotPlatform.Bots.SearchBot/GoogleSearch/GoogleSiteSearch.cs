using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using HtmlAgilityPack;

namespace TelegramBotPlatform.Bots.SearchBot.GoogleSearch
{
	public class GoogleSiteSearch : IGoogleSearchEngine
	{
		private const string Url = "http://www.google.com/search?num=100&q={0}";
		public IEnumerable<HtmlNode> Search(string queryString)
		{
			var result = new HtmlWeb().Load(string.Format(Url, queryString));
			var nodes = result.DocumentNode.SelectNodes("//html//body//div[@class='g']");
			return nodes?.ToList();
		}
	}
}