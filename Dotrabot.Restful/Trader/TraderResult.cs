using Newtonsoft.Json;

namespace Dotrabot.Restful.Trader
{
    public class TraderResult
    {
        [JsonProperty(PropertyName = "id")]
        public Int64 Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }
    }
}