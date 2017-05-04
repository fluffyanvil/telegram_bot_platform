using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegramBotPlatform.Core.BingWebSearchApi.Response;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.BingWebSearchApi
{
	public class BingWebSearchApi : IBingWebSearchApi, IWebSearchEngine
	{
		private string _ocpApimSubscriptionKeyHeader = "Ocp-Apim-Subscription-Key";
		private readonly HttpClient _httpClient;

		private string _queryPattern =
			"https://api.cognitive.microsoft.com/bing/v5.0/search?q={0}&count={1}&offset={2}&mkt={3}&safesearch={4}";

		public BingWebSearchApi(string subscriptionKey)
		{
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Add(_ocpApimSubscriptionKeyHeader, subscriptionKey);
			
		}

		public async Task<IWebSearchResult> SearchAsync(string query, int count = 10, int offset = 0, string market = "en-US", string safesearch = "Off")
		{
			if (string.IsNullOrEmpty(query)) return new BingWebSearchApiResult();
			var url = string.Format(_queryPattern, query, count, offset, market, safesearch);
			var response = await _httpClient.GetStringAsync(url);
			var result = new BingWebSearchApiResult();
		    if (string.IsNullOrEmpty(response)) return result;
		    var searchResult = JsonConvert.DeserializeObject<SearchResult>(response);
		    result.SearchResult = searchResult;
		    return result;
		}

	    Task<IWebSearchResult> IWebSearchEngine.SearchAsync(string query)
	    {
	        return SearchAsync(query);
        }
	}
}