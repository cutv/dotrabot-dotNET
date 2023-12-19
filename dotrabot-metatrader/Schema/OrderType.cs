namespace Dotrabot.Metatrader.Schema
{


    public class OrderType
    {
        public static readonly OrderType ORDER_TYPE_BUY = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_SELL = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_BUY_LIMIT = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_SELL_LIMIT = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_BUY_STOP = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_SELL_STOP = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_BUY_STOP_LIMIT = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_SELL_STOP_LIMIT = new OrderType(1);
        public static readonly OrderType ORDER_TYPE_CLOSE_BY = new OrderType(1);

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
                (Value) = (value);

    }
}
