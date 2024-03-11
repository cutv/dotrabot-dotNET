using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.StompClient.Schema
{
    public class TradingServerMessage
    {
        public String serverName { get; set; }

        public List<String> symbols { get; set; }
    }
}
