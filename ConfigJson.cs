using Newtonsoft.Json;

namespace GangstaXSCPBot
{
    struct ConfigJson
    {
        [JsonProperty("Token")]
        public string Token { get; private set; }
        [JsonProperty("Prefix")]
        public string Prefix { get; private set; }
        [JsonProperty("APILink")]
        public string APILink { get; private set; }
        [JsonProperty("BotTitle")]
        public string BotTitle { get; private set; }
    }
}
