using Newtonsoft.Json;

namespace TelegramBotPlatform.Core.TelegramBotAggregator.Config
{
    [JsonObject]
    public class BotAggregatorConfig
    {
        [JsonProperty("botAccessTokens")]
        public string[] BotAccessTokens { get; set; }

        public static BotAggregatorConfig SampleBotAggregatorConfig => new BotAggregatorConfig
        {
            BotAccessTokens = new[]
            {
                "278757304:AAGTmHVA_zMgmwM6THrKphVGNkucV8kYX0U",
                "274034104:AAEHvq5oICECV1Z-6frNmSLxEZ494i44rfI"
            }
        };
    }
}