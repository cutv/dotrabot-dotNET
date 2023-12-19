namespace Dotrabot.Metatrader.Schema
{


    public class OrderState
    {
        public static readonly OrderState ORDER_STATE_STARTED = new OrderState(1);
        public static readonly OrderState ORDER_STATE_PLACED = new OrderState(1);
        public static readonly OrderState ORDER_STATE_CANCELED = new OrderState(1);
        public static readonly OrderState ORDER_STATE_PARTIAL = new OrderState(1);
        public static readonly OrderState ORDER_STATE_FILLED = new OrderState(1);
        public static readonly OrderState ORDER_STATE_REJECTED = new OrderState(1);
        public static readonly OrderState ORDER_STATE_EXPIRED = new OrderState(1);
        public static readonly OrderState ORDER_STATE_REQUEST_ADD = new OrderState(1);
        public static readonly OrderState ORDER_STATE_REQUEST_MODIFY = new OrderState(1);
        public static readonly OrderState ORDER_STATE_REQUEST_CANCEL = new OrderState(1);

        public static IEnumerable<OrderState> Values
        {
            get
            {
                yield return ORDER_STATE_STARTED;
                yield return ORDER_STATE_PLACED;
                yield return ORDER_STATE_CANCELED;
                yield return ORDER_STATE_PARTIAL;
                yield return ORDER_STATE_FILLED;
                yield return ORDER_STATE_REJECTED;
                yield return ORDER_STATE_EXPIRED;
                yield return ORDER_STATE_REQUEST_ADD;
                yield return ORDER_STATE_REQUEST_MODIFY;
                yield return ORDER_STATE_REQUEST_CANCEL;
            }
        }

        public int Value { get; private set; }

        private OrderState(int value) =>
                (Value) = (value);

    }
}
