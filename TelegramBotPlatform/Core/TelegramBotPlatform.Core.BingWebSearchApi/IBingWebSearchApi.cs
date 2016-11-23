using System.Threading.Tasks;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.BingWebSearchApi
{
	public interface IBingWebSearchApi
	{
		Task<IWebSearchResult> SearchAsync(string query, int count = 10, int offset = 0, string market = "en-US", string safesearch = "Off");
	}
}