using Rananu.Shared;
using Rananu.Shared.Converter;
using System.Text.Json.Serialization;

namespace Dotrabot.MT5.Schema
{

    [JsonConverter(typeof(EnumConverter<int>))]
    public class OrderState: IEnum<int>
    {
        public static readonly OrderState ORDER_STATE_STARTED = new OrderState(0);
        public static readonly OrderState ORDER_STATE_PLACED = new OrderState(1);
        public static readonly OrderState ORDER_STATE_CANCELED = new OrderState(2);
        public static readonly OrderState ORDER_STATE_PARTIAL = new OrderState(3);
        public static readonly OrderState ORDER_STATE_FILLED = new OrderState(4);
        public static readonly OrderState ORDER_STATE_REJECTED = new OrderState(5);
        public static readonly OrderState ORDER_STATE_EXPIRED = new OrderState(6);
        public static readonly OrderState ORDER_STATE_REQUEST_ADD = new OrderState(7);
        public static readonly OrderState ORDER_STATE_REQUEST_MODIFY = new OrderState(8);
        public static readonly OrderState ORDER_STATE_REQUEST_CANCEL = new OrderState(9);

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
                Value = value;
    }
}
