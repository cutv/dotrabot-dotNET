using Newtonsoft.Json;

namespace Dotrabot.StompClient.Schema
{
    
    public class Message
    {
        [JsonProperty(PropertyName = "type")]
        public PayloadType Type { get; set; }

        [JsonProperty(PropertyName = "payload")]
        public String Payload { get; set; }
    }
}
