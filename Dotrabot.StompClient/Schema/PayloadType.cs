using Rananu.Shared;
using Rananu.Shared.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dotrabot.StompClient.Schema
{
    public enum PayloadType
    {
        [EnumMember(Value = "TradingServer")]
        TradingServer,

        [EnumMember(Value = "Trade")]
        Trade,

        [EnumMember(Value = "Trader")]
        Trader,

        [EnumMember(Value = "Terminal")]
        Terminal,

        [EnumMember(Value = "Account")]
        Account,

        [EnumMember(Value = "Balance")]
        Balance
    }
}
