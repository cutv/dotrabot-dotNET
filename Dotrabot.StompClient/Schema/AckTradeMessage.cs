namespace Dotrabot.StompClient.Schema
{
    public class AckTradeMessage : TradeMessage
    {
        public int? last_error { get; set; }
        public int? retcode { get; set; }
    }
}