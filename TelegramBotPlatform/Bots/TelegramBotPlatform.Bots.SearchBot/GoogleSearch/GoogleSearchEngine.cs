using System.Collections.Generic;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;

namespace TelegramBotPlatform.Bots.SearchBot.GoogleSearch
{
	public class GoogleSearchEngine
	{
		private CustomsearchService _googleCustomsearchService;
		private string _googleSearchApiKey = "AIzaSyCfLsEYZ17kPnKGZwN5p90MDQfAuo65AlQ";
		private string _googleSearchApiApplicationName = "SearchApiKey";
		public GoogleSearchEngine()
		{
			_googleCustomsearchService = new CustomsearchService(new BaseClientService.Initializer
			{
				ApiKey = _googleSearchApiKey,
				ApplicationName = _googleSearchApiApplicationName
			});
		}

		public IEnumerable<Result> Search(string query)
		{
			if (string.IsNullOrEmpty(query)) return new List<Result>();
			var listRequest = _googleCustomsearchService.Cse.List(query);
			listRequest.Cx = "010413868545992504530:xysqnrglcne";
			var search = listRequest.Execute();
			return search.Items;
		}
	}
}