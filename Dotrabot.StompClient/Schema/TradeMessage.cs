using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.StompClient.Schema
{
    public class TradeMessage:BaseMessage
    {
        public long? traderId { get; set; }
        public String? serverName { get; set; }
        public ulong? action { get; set; }
        public ulong? magic { get; set; }
        public ulong? order { get; set; }
        public String? symbol { get; set; }
        public double? volume { get; set; }
        public double? price { get; set; }
        public double? stoplimit { get; set; }
        public double? sl { get; set; }
        public double? tp { get; set; }
        public ulong? deviation { get; set; }
        public int? type { get; set; }
        public int? type_filling { get; set; }
        public DateTime? expiration { get; set; }
        public String? comment { get; set; }
        public ulong? position { get; set; }
        public ulong? position_by { get; set; }
        public int? slippage { get; set; }
        public ulong? terminalCreatedAt { get; set; }
    }
}
