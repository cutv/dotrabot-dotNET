using Microsoft.VisualBasic.Logging;
using Netina.Stomp.Client;
using Netina.Stomp.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotrabot.NET
{
    public class DotrabotStompClient
    {
        private IStompClient _client;

        public DotrabotStompClient(string url)
        {
            _client = new StompClient(url); 
        }


        public async Task ConnectAsync()
        {
            var headers = new Dictionary<string, string>();
            await _client.ConnectAsync(headers);
        }

        public async Task SubscribeAsync(String traderId,Action<Object> handler)
        {
            await _client.SubscribeAsync<object>($"/copiers/{traderId}", new Dictionary<string, string>(), (sender, dto) =>
            {
                handler.Invoke(dto);
            });
        }

        public async Task TradeAsync(Trade trade)
        {
            var headers = new Dictionary<string, string>();
            await _client.SendAsync(trade,"/masters/broadcast", headers);
        }

    }
}
