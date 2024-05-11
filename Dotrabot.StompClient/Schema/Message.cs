using Newtonsoft.Json;

namespace Dotrabot.StompClient.Schema
{

    public class Message
    {
        [JsonProperty(PropertyName = "type")]
        public PayloadType Type { get; set; }

        [JsonProperty(PropertyName = "data")]
        public String Data { get; set; }
    }
}
