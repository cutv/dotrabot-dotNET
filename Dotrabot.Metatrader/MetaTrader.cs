using Dotrabot.MT5.Schema;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace Dotrabot
{
    public class MetaTrader
    {
        private PublisherSocket _publisherSocket;
        private PullSocket _pullSocket;


        public MetaTrader()
        {
            _publisherSocket = new PublisherSocket("@tcp://*:863");
            _pullSocket = new PullSocket("@tcp://*:831");
        }


        public void ReceiveAsync(Action<String> onReceived)
        {
            new Task(() =>
            {
                while (true)
                {
                    string payload = _pullSocket.ReceiveFrameString();
                    onReceived.Invoke(payload);
                }
            }, TaskCreationOptions.LongRunning).Start();
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
