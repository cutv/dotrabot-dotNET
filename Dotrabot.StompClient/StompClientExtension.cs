using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client;
using Netina.Stomp.Client.Interfaces;

namespace Dotrabot.StompClient
{
    public static class StompClientExtension
    {
        public static Task SubscribeAsync<T>(this IStompClient stompClient,long traderId, Action<T> onMessage)
        {
            return stompClient.SubscribeAsync<T>($"/traders/{traderId}", new Dictionary<String, String>(), (sender, message) =>
            {
                onMessage.Invoke(message);
            });
        }

        public static Task BroadcastTradeAsync(this IStompClient stompClient, TradeMessage message)
        {
            return stompClient.SendAsync(message, "/trades", new Dictionary<String, String>());
        }
        public static Task AckTradeAsync(this IStompClient stompClient, ulong tradeId, AckTradeMessage message)
        {
            return stompClient.SendAsync(message, $"/trades/{tradeId}/ack", new Dictionary<String, String>());
        }
         
        public static Task UpdateTraderAsync(this IStompClient stompClient, long traderId, TraderMessage message)
        {
            return stompClient.SendAsync(message, $"/traders/{traderId}", new Dictionary<String, String>());
        }

        public static Task CreateOrUpdateTradingServerAsync(this IStompClient stompClient, TradingServerMessage message)
        {
            return stompClient.SendAsync(message, $"/trading-servers", new Dictionary<String, String>());
        }
    }
   
}
