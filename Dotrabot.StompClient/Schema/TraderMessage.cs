using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.StompClient.Schema
{
    public class TraderMessage
    {
        [JsonProperty(PropertyName = "account")]
        public Dictionary<String, Object>? Account { get; set; }
 
        [JsonProperty(PropertyName = "terminal")]
        public Dictionary<String, Object>? Terminal { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public Dictionary<String, Object>? Balance { get; set; }
    }
}
