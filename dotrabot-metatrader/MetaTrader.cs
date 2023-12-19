using NetMQ;
using NetMQ.Sockets;

namespace Dotrabot.Metatrader
{
    public class MetaTrader
    {
        private ResponseSocket _responseSocket;

        public MetaTrader(String connectionString)
        {
            _responseSocket = new ResponseSocket(connectionString);
            _responseSocket.SendReady += _responseSocket_SendReady;
        }

        private void _responseSocket_SendReady(object? sender, NetMQSocketEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ReceiveAsync(Action<String> handler)
        {

            new Task(() =>
            {
                while (true)
                {
                    String payload = _responseSocket.ReceiveFrameString();
                    handler.Invoke(payload);
                    _responseSocket.SendFrameEmpty();
                }
            }, TaskCreationOptions.LongRunning).Start();

        }

        public Task SendAsync(String payload)
        {
            return Task.Run(() => { _responseSocket.SendFrame(payload); });
        }



    }
}
