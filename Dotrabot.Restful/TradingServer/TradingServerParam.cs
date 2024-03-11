namespace Dotrabot.Restful.TradingServer
{
    public class TradingServerParam
    {
        public String serverName { get; set; }
        public ISet<String> symbols { get; set; }
    }
}