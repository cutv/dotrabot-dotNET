using Newtonsoft.Json;
using Rananu.Shared;
using Rananu.Shared.Converter;

namespace Dotrabot.MT5.Schema
{

    [JsonConverter(typeof(EnumConverter<int>))]
    public class OrderTypeTime : IEnum<int>
    {
        public static readonly OrderTypeTime ORDER_TIME_GTC = new OrderTypeTime(0);
        public static readonly OrderTypeTime ORDER_TIME_DAY = new OrderTypeTime(1);
        public static readonly OrderTypeTime ORDER_TIME_SPECIFIED = new OrderTypeTime(2);
        public static readonly OrderTypeTime ORDER_TIME_SPECIFIED_DAY = new OrderTypeTime(3);

        public static IEnumerable<OrderTypeTime> Values
        {
            get
            {
                yield return ORDER_TIME_GTC;
                yield return ORDER_TIME_DAY;
                yield return ORDER_TIME_SPECIFIED;
                yield return ORDER_TIME_SPECIFIED_DAY;
            }
        }

        public int Value { get; private set; }

        private OrderTypeTime(int value) =>
                Value = value;

    }
}
