using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client;
using Netina.Stomp.Client.Interfaces;
using Rananu.Shared;
using System.Text;

namespace Dotrabot.StompClient
{
    public static class StompClientExtension
    {
        public static String Authorization;
        public static Task SubscribeMeAsync<T>(this IStompClient stompClient, long traderId, Action<T> onMessage)
        {
            return stompClient.SubscribeAsync<T>($"/traders/{traderId}", new Dictionary<String, String>(), (sender, message) =>
            {
                onMessage.Invoke(message);
            });
        }

        public static Task SubscribeEAAsync<T>(this IStompClient stompClient, Action<T> onMessage)
        {
            return stompClient.SubscribeAsync<T>($"/ea", new Dictionary<String, String>(), (sender, message) =>
            {
                onMessage.Invoke(message);
            });
        }

        public static Task SendAsync(this IStompClient stompClient, String topic, String payload)
        {
            return stompClient.SendAsync(payload, topic, NewHeaders(payload));
        }

        public static IDictionary<String, String> NewHeaders(string payload)
        {
            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add("content-type", "application/json;charset=UTF-8");
            headers.Add("content-length", Encoding.UTF8.GetByteCount(payload).ToString());
            //headers.Add("Authorization", Authorization);
            return headers;
        }

    }

}
