using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using TelegramBotPlatform.Core.Common.Interfaces;

namespace TelegramBotPlatform.Core.GoogleWebSearch
{
    public class GoogleWebSearch : IGoogleWebSearch
    {
        private readonly CustomsearchService _customsearchService;
        private readonly string _cx;

        public GoogleWebSearch(string apiKey, string cx)
        {
            _cx = cx;
            _customsearchService = new CustomsearchService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey
            });
        }

        public async Task<IWebSearchResult> SearchAsync(string query, bool searchImages)
        {
            if (string.IsNullOrEmpty(query)) return new GoogleWebSearchResult {Results = new List<Result>()};
            var listRequest = _customsearchService.Cse.List(query);
            listRequest.Cx = _cx;
            if (searchImages)
                listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            listRequest.Safe = CseResource.ListRequest.SafeEnum.Off;
            try
            {
                var search = await listRequest.ExecuteAsync();
                return new GoogleWebSearchResult { Results = search.Items };
            }
            catch (Exception exception)
            {
                
                throw;
            }
            
        }
    }
}