using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.InlineQueryResults;
using TelegramBotPlatform.Core.Common.Interfaces;
using TelegramBotPlatform.Core.SearchBot.Converters;

namespace TelegramBotPlatform.Core.SearchBot
{
    public class WebSearchAggregator : IWebSearchAggregator
    {
        private IEnumerable<IWebSearchEngine> _engines;

        public WebSearchAggregator(params IWebSearchEngine[] engines)
        {
            _engines = engines.ToList();
        }

        public async Task<IEnumerable<InlineQueryResult>> Search(string query, bool searchImages = false)
        {
            var inlineQueryResults = new List<InlineQueryResult>();
            if (string.IsNullOrEmpty(query)) return inlineQueryResults;

            foreach (var webSearchEngine in _engines)
            {
                var results = await webSearchEngine.SearchAsync(query);
                inlineQueryResults.AddRange(WebSearchResultToInlineQueryResultConverter.Convert(results));
            }

            return inlineQueryResults;
        }
    }
}