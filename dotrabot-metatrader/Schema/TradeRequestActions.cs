namespace Dotrabot.Metatrader.Schema
{


    public class TradeRequestActions
    {
        public static readonly TradeRequestActions TRADE_ACTION_DEAL = new TradeRequestActions(1);
        public static readonly TradeRequestActions TRADE_ACTION_PENDING = new TradeRequestActions(1);
        public static readonly TradeRequestActions TRADE_ACTION_SLTP = new TradeRequestActions(1);
        public static readonly TradeRequestActions TRADE_ACTION_MODIFY = new TradeRequestActions(1);
        public static readonly TradeRequestActions TRADE_ACTION_REMOVE = new TradeRequestActions(1);
        public static readonly TradeRequestActions TRADE_ACTION_CLOSE_BY = new TradeRequestActions(1);

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
                (Value) = (value);

    }
}
