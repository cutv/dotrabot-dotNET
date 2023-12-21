using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client.Messages;

namespace Dotrabot.StompClient
{
    public class DotrabotStompClient
    {
        private Netina.Stomp.Client.StompClient _stompClient;

        public DotrabotStompClient()
        {
            _stompClient = new Netina.Stomp.Client.StompClient("ws://localhost:8080/metratrader");
        }

        public Task ConnectAsync(Action onConnected)
        {
            _stompClient.OnConnect += (send, message) =>
            {
                onConnected.Invoke();
            };
            return _stompClient.ConnectAsync(new Dictionary<String, String>());
        }

        public Task SubscribeAsync<T>(long traderId, Action<T> onMessage)
        {
            return _stompClient.SubscribeAsync<T>($"traders/{traderId}", new Dictionary<String, String>(), (sender, message) =>
            {
                onMessage.Invoke(message);
            });
        }

        public Task BroadcastTradeAsync(TradeMessage message)
        {
            return _stompClient.SendAsync(message, "/trades", new Dictionary<String, String>());
        }
        public Task AckTradeAsync(String tradeId, AckTradeMessage message)
        {
            return _stompClient.SendAsync(message, $"/trades/{tradeId}/ack", new Dictionary<String, String>());
        }

    }
}
