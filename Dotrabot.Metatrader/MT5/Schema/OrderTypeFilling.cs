using Newtonsoft.Json;
using Rananu.Shared;
using Rananu.Shared.Converter;

namespace Dotrabot.MT5.Schema
{

    [JsonConverter(typeof(EnumConverter<int>))]
    public class OrderTypeFilling : IEnum<int>
    {
        public static readonly OrderTypeFilling ORDER_FILLING_FOK = new OrderTypeFilling(0);
        public static readonly OrderTypeFilling ORDER_FILLING_IOC = new OrderTypeFilling(1);
        public static readonly OrderTypeFilling ORDER_FILLING_BOC = new OrderTypeFilling(3);
        public static readonly OrderTypeFilling ORDER_FILLING_RETURN = new OrderTypeFilling(2);

        public static IEnumerable<OrderTypeFilling> Values
        {
            get
            {
                yield return ORDER_FILLING_FOK;
                yield return ORDER_FILLING_IOC;
                yield return ORDER_FILLING_BOC;
                yield return ORDER_FILLING_RETURN;
            }
        }

        public int Value { get; private set; }

        private OrderTypeFilling(int value) =>
                (Value) = (value);

    }
}
