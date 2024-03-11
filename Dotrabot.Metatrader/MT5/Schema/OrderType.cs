using Newtonsoft.Json;
using Rananu.Shared;
using Rananu.Shared.Converter;

namespace Dotrabot.MT5.Schema
{

    [JsonConverter(typeof(EnumConverter<int>))]
    public class OrderType : IEnum<int>
    {
        public static readonly OrderType ORDER_TYPE_BUY = new OrderType(0);
        public static readonly OrderType ORDER_TYPE_SELL = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_BUY_LIMIT = new OrderType(2);
        public static readonly OrderType ORDER_TYPE_SELL_LIMIT = new OrderType(3);
        public static readonly OrderType ORDER_TYPE_BUY_STOP = new OrderType(4);
        public static readonly OrderType ORDER_TYPE_SELL_STOP = new OrderType(5);
        public static readonly OrderType ORDER_TYPE_BUY_STOP_LIMIT = new OrderType(6);
        public static readonly OrderType ORDER_TYPE_SELL_STOP_LIMIT = new OrderType(7);
        public static readonly OrderType ORDER_TYPE_CLOSE_BY = new OrderType(8);

        public static IEnumerable<OrderType> Values
        {
            get
            {
                yield return ORDER_TYPE_BUY;
                yield return ORDER_TYPE_SELL;
                yield return ORDER_TYPE_BUY_LIMIT;
                yield return ORDER_TYPE_SELL_LIMIT;
                yield return ORDER_TYPE_BUY_STOP;
                yield return ORDER_TYPE_SELL_STOP;
                yield return ORDER_TYPE_BUY_STOP_LIMIT;
                yield return ORDER_TYPE_SELL_STOP_LIMIT;
                yield return ORDER_TYPE_CLOSE_BY;
            }
        }

        public int Value { get; private set; }

        private OrderType(int value) =>
                Value = value;

    }
}
