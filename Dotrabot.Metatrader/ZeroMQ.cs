using Dotrabot.MT5.Schema;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace Dotrabot
{
    public class ZeroMQ
    {
        private PublisherSocket _publisherSocket;
        private PullSocket _pullSocket;
        private NetMQPoller _netMQPoller;

        public bool Receive { get; set; }
        public bool EnableReceiving { get; set; }
        public Action<String> OnReceived { get; set; }
        public ZeroMQ()
        {
            _publisherSocket = new PublisherSocket("@tcp://*:863");
            _pullSocket = new PullSocket("@tcp://*:831");
            _netMQPoller = new NetMQPoller { _publisherSocket, _pullSocket };
            _pullSocket.ReceiveReady += _pullSocket_ReceiveReady;
            _netMQPoller.RunAsync();
        }

        private void _pullSocket_ReceiveReady(object? sender, NetMQSocketEventArgs e)
        {
            if (EnableReceiving)
            {
                string payload = _pullSocket.ReceiveFrameString();
                OnReceived.Invoke(payload);
            }
        }

        public Task SendAsync(string topic, string payload)
        {
            return Task.Run(() => _publisherSocket.SendMoreFrame(topic).SendFrame(payload));
        }
        public Task SendAsync(string payload)
        {
            return Task.Run(() => _publisherSocket.SendMoreFrame("").SendFrame(payload));
        }
    }
}
