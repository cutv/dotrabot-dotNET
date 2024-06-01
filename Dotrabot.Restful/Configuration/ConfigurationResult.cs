using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.Restful.Configuration
{
    public class ConfigurationResult
    {
        public String Id { get; set; }
        public EAConfiguration ea { get; set; }
        public MiddlewareConfiguration middleware { get; set; }

    }

    public class EAConfiguration
    {
        public Int64 syncServerInterval { get; set; }
        public Int64 syncTraderInterval { get; set; }
        public Int64 syncPositionInterval { get; set; }
        public Int64 syncHistoryOrderInterval { get; set; }
        public Int64 syncHistoryDealInterval { get; set; }
    }
    public class MiddlewareConfiguration
    {
        public IEnumerable<String> subscribe_topics { get; set; }
        [JsonProperty("topic")]
        public String Topic { get; set; }
    }

}
