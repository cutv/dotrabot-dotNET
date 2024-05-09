using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.Restful
{
    public class Result<T>
    {
        [JsonProperty(PropertyName = "errorCode")]
        public Int32 ErrorCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public String Message { get; set; }

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public Dictionary<String, String> Errors { get; set; }
    }
}
