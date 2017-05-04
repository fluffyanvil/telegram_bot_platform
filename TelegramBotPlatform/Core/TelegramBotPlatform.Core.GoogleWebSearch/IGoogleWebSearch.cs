using System.Threading.Tasks;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.GoogleWebSearch
{
    public interface IGoogleWebSearch
    {
        Task<IWebSearchResult> SearchAsync(string query, bool searchImages = false);
    }
}