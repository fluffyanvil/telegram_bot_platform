using TelegramBotPlatform.Core.Common.Enums;

namespace TelegramBotPlatform.Core.Common.Interfaces
{
    public interface IWebSearchResult
    {
        SearchEngine SearchEngine { get; } 
        object Result { get; }
    }
}