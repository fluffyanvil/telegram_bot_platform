using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types.InlineQueryResults;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.SearchBot
{
    public interface IWebSearchAggregator
    {
        Task<IEnumerable<InlineQueryResult>> Search(string query, bool searchImages = false);
    }
}