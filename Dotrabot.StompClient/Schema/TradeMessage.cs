using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.StompClient.Schema
{
    public class TradeMessage : BaseMessage
    {
        public long? traderId { get; set; }
        public String? serverName { get; set; }
        public ulong? action { get; set; }
        public ulong? magic { get; set; }
        public ulong? order { get; set; }
        public String? symbol { get; set; }
        public double? volume { get; set; }
        public double? price { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
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
        public ulong? open_time { get; set; }

        public ulong? deal { get; set; }           // Deal ticket
        public int? order_type { get; set; }     // Order type
        public int? order_state { get; set; }    // Order state
        public int? deal_type { get; set; }      // Deal type
        public int? time_type { get; set; }      // Order type by action period
        public String? time_expiration { get; set; }// Order expiration time
        public double? price_trigger { get; set; }  // Stop limit order activation price
        public double? price_sl { get; set; }       // Stop Loss level
        public double? price_tp { get; set; }       // Take Profit level
    }
}
