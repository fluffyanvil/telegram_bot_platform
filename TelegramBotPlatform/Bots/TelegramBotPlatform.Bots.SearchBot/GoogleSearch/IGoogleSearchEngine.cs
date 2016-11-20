using System.Collections.Generic;
using HtmlAgilityPack;

namespace TelegramBotPlatform.Bots.SearchBot.GoogleSearch
{
	public interface IGoogleSearchEngine
	{
		IEnumerable<HtmlNode> Search(string queryString);
	}
}