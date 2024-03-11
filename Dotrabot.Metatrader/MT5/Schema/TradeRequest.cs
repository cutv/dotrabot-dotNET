namespace Dotrabot.MT5.Schema
{
    public class TradeRequest
    {
        //private   TradeRequestActions action;           // Trade operation type
        //   private ulong magic;            // Expert Advisor ID (magic number)
        //   private ulong order;            // Order ticket
        //   private string symbol;           // Trade symbol
        //   private double volume;           // Requested volume for a deal in lots
        //   private double price;            // Price
        //   private double stoplimit;        // StopLimit level of the order
        //   private double sl;               // Stop Loss level of the order
        //   private double tp;               // Take Profit level of the order
        //   private ulong deviation;        // Maximal possible deviation from the requested price
        //   private OrderType type;             // Order type
        //   private OrderTypeFilling type_filling;     // Order execution type
        //   private OrderTypeTime type_time;        // Order expiration type
        //   private DateTime expiration;       // Order expiration time (for the orders of ORDER_TIME_SPECIFIED type)
        //   private String comment;          // Order comment
        //   private ulong position;         // Position ticket
        //   private ulong position_by;      // The ticket of an opposite position
    
        public TradeRequestActions? action { get; set; }
        public ulong? magic { get; set; }
        public ulong? order { get; set; }
        public String? symbol { get; set; }
        public double? volume { get; set; }
        public double? price { get; set; }
        public double? stoplimit { get; set; }
        public double? sl { get; set; }
        public double? tp { get; set; }
        public ulong? deviation { get; set; }
        public OrderType? type { get; set; }
        public OrderTypeFilling? type_filling { get; set; }
        public DateTime? expiration { get; set; }
        public String? comment { get; set; }
        public ulong? position { get; set; }
        public ulong? position_by { get; set; }

    }
}