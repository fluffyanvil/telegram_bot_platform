using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1.Data;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.GoogleWebSearch
{
    public interface IGoogleWebSearch
    {
        Task<IWebSearchResult> SearchAsync(string query, bool searchImages = false);
    }
}