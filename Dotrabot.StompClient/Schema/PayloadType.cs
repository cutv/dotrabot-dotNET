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
        [EnumMember(Value = "pong")]
        Pong,

        [EnumMember(Value = "trading_server")]
        TradingServer,

        [EnumMember(Value = "trade")]
        Trade,

        [EnumMember(Value = "ack_trade")]
        AckTrade,

        [EnumMember(Value = "trader")]
        Trader,

        [EnumMember(Value = "terminal")]
        Terminal,

        [EnumMember(Value = "account")]
        Account,

        [EnumMember(Value = "balance")]
        Balance
    }
}
