using Dotrabot.MT5.Schema;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;

namespace Dotrabot
{
    public class MetaTrader
    {
        private PushSocket _pushSocket;
        private PullSocket _pullSocket;


        public MetaTrader()
        {
            _pushSocket = new PushSocket("@tcp://*:863");
            _pullSocket = new PullSocket(">tcp://localhost:831");
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

        public void SendTradeAsync(TradeRequest trade)
        {
            String json = JsonConvert.SerializeObject(trade);
            _pushSocket.SendFrame(json);
        }
        public void SendTradeAsync(string json)
        {
            _pushSocket.SendFrame(json);
        }



    }
}
