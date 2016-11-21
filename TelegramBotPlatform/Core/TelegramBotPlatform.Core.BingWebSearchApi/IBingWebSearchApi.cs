using System.Threading.Tasks;
using TelegramBotPlatform.Core.BingWebSearchApi.Response;

namespace TelegramBotPlatform.Core.BingWebSearchApi
{
	public interface IBingWebSearchApi
	{
		Task<BingWebSearchResult> SearchAsync(string query, int count = 10, int offset = 0, string market = "en-US", string safesearch = "Off");
	}
}