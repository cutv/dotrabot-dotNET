using Dotrabot.Restful;
using Dotrabot.Restful.Trader;
using Dotrabot.StompClient;
using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client.Interfaces;
using Rananu.Shared;
using System.Diagnostics;
using System.Windows;

namespace Dotrabot.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDotrabotClientConfig
    {
        private MetaTrader _metaTrader = new MetaTrader();
        //IStompClient _stompClient = new Netina.Stomp.Client.StompClient("ws://localhost:8080/metatrader");
        IStompClient _stompClient = new Netina.Stomp.Client.StompClient("ws://14.225.207.213/metatrader");
        private TraderResult _trader;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public string Authorization => txtClientSecret.Text;

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TraderFactory traderFactory = new TraderFactory(this);
            _trader = await traderFactory.findByMe();
            tblName.Text = _trader.Name;
            _stompClient.OnConnect += async (sender, message) =>
                {
                    //Debug.WriteLine("Connected");
                    //await _stompClient.SubscribeAsync<object>(_trader.Id, (message) =>
                    //{
                    //    var payload = message.ToString();
                    //    if (payload != null)
                    //        _metaTrader.SendAsync(payload);
                    //});
                };
            _stompClient.OnError += (sender, message) =>
            {
                Debug.WriteLine(message.ToString());
            };
            _stompClient.OnClose += (sender, message) =>
            {
                Debug.WriteLine(message.ToString());
            };
            _stompClient.OnReconnect += (sender, message) =>
            {
                Debug.WriteLine(message.ToString());
            };
            await _stompClient.ConnectAsync(new Dictionary<String, String>());


            _metaTrader.ReceiveAsync(async (message) =>
            {
                Debug.WriteLine(message);
                if (String.IsNullOrEmpty(message))
                    return;
                var data = NewtonsoftConvert.Instance.DeserializeObject<Message>(message);
                switch (data.Type)
                {
                    case PayloadType.TradingServer:
                        var tradingServerPayload = NewtonsoftConvert.Instance.DeserializeObject<TradingServerMessage>(data.Payload);
                        await _stompClient.CreateOrUpdateTradingServerAsync(tradingServerPayload);
                        break;
                    case PayloadType.Trade:
                        var tradeMessage = NewtonsoftConvert.Instance.DeserializeObject<TradeMessage>(data.Payload);
                        tradeMessage.traderId = _trader.Id;
                        await _stompClient.BroadcastTradeAsync(tradeMessage);
                        break;
                    case PayloadType.AckTrade:
                        var ackMessage = NewtonsoftConvert.Instance.DeserializeObject<AckTradeMessage>(data.Payload);
                        ackMessage.traderId = _trader.Id;
                        await _stompClient.AckTradeAsync(ackMessage.magic.Value, ackMessage);
                        break;
                    case PayloadType.Pong:
                        var pongAtMillis = NewtonsoftConvert.Instance.DeserializeObject<Int64>(data.Payload);
                        break;
                    case PayloadType.Trader:
                        break;
                    case PayloadType.Account:
                    case PayloadType.Terminal:
                    case PayloadType.Balance:
                        var traderPayload = NewtonsoftConvert.Instance.DeserializeObject<Dictionary<String, String>>(data.Payload);
                        var traderMessage = new TraderMessage();
                        if (data.Type == PayloadType.Account)
                            traderMessage.Account = traderPayload;
                        if (data.Type == PayloadType.Terminal)
                            traderMessage.Terminal = traderPayload;
                        if (data.Type == PayloadType.Balance)
                            traderMessage.Balance = traderPayload;
                        await _stompClient.UpdateTraderAsync(_trader.Id, traderMessage);

                        break;
                }

            });


        }


    }
}