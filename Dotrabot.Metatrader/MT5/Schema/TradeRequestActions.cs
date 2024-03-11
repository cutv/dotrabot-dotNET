using Newtonsoft.Json;
using Rananu.Shared;
using Rananu.Shared.Converter;

namespace Dotrabot.MT5.Schema
{


    [JsonConverter(typeof(EnumConverter<int>))]
    public class TradeRequestActions: IEnum<int>
    {
        public static readonly TradeRequestActions TRADE_ACTION_DEAL = new TradeRequestActions(1);
        public static readonly TradeRequestActions TRADE_ACTION_PENDING = new TradeRequestActions(5);
        public static readonly TradeRequestActions TRADE_ACTION_SLTP = new TradeRequestActions(6);
        public static readonly TradeRequestActions TRADE_ACTION_MODIFY = new TradeRequestActions(7);
        public static readonly TradeRequestActions TRADE_ACTION_REMOVE = new TradeRequestActions(8);
        public static readonly TradeRequestActions TRADE_ACTION_CLOSE_BY = new TradeRequestActions(10);

        public static IEnumerable<TradeRequestActions> Values
        {
            get
            {
                yield return TRADE_ACTION_DEAL;
                yield return TRADE_ACTION_PENDING;
                yield return TRADE_ACTION_SLTP;
                yield return TRADE_ACTION_MODIFY;
                yield return TRADE_ACTION_REMOVE;
                yield return TRADE_ACTION_CLOSE_BY;
            }
        }

        public int Value { get; private set; }

        private TradeRequestActions(int value) =>
                Value = value;

    }
}
