using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client;
using Netina.Stomp.Client.Interfaces;

namespace Dotrabot.StompClient
{
    public static class StompClientExtension
    {
        public static Task SubscribeAsync<T>(this IStompClient stompClient, long traderId, Action<T> onMessage)
        {
            return stompClient.SubscribeAsync<T>($"/traders/{traderId}", new Dictionary<String, String>(), (sender, message) =>
            {
                onMessage.Invoke(message);
            });
        }

        public static Task BroadcastTradeAsync(this IStompClient stompClient, Dictionary<String, Object> payload)
        {
            return stompClient.SendAsync(payload, "/trades", new Dictionary<String, String>());
        }
        public static Task AckTradeAsync(this IStompClient stompClient, long tradeId, Dictionary<String, Object> payload)
        {
            return stompClient.SendAsync(payload, $"/trades/{tradeId}/ack", new Dictionary<String, String>());
        }

        public static Task UpdateTraderAsync(this IStompClient stompClient, long traderId, Dictionary<String, Object> payload)
        {
            return stompClient.SendAsync(payload, $"/traders/{traderId}", new Dictionary<String, String>());
        }

        public static Task CreateOrUpdateTradingServerAsync(this IStompClient stompClient, Dictionary<String, Object> payload)
        {
            return stompClient.SendAsync(payload, $"/trading-servers", new Dictionary<String, String>());
        }
    }

}
