using Newtonsoft.Json;

namespace Dotrabot.StompClient.Schema
{

    public class Message
    {
        [JsonProperty(PropertyName = "type")]
        public PayloadType Type { get; set; }

        [JsonProperty(PropertyName = "data")]
        public Dictionary<String, Object> Data { get; set; }
    }
}
