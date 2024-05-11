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
        IStompClient _stompClient = new Netina.Stomp.Client.StompClient("ws://localhost:8080/metatrader");
        //IStompClient _stompClient = new Netina.Stomp.Client.StompClient("ws://14.225.207.213/metatrader");
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


            _metaTrader.ReceiveAsync((Action<string>)(async (message) =>
            {
                Debug.WriteLine(message);
                if (string.IsNullOrEmpty(message))
                    return;
                var payload = NewtonsoftConvert.Instance.DeserializeObject<Message>(message);
                PayloadType type = payload.Type;
                string json = payload.Data;
                switch (type)
                {
                    case PayloadType.TradingServer:
                        await _stompClient.CreateOrUpdateTradingServerAsync(json);
                        break;
                    //case PayloadType.Trade:
                    //    data.Add("trader_id", (object)_trader.Id);
                    //    await _stompClient.BroadcastTradeAsync(data);
                    //    break;
                    //case PayloadType.AckTrade:
                    //    data.Add("trader_id", (object)_trader.Id);
                    //    await _stompClient.AckTradeAsync((long)data.GetValueOrDefault<string, object>("magic"), data);
                    //    break;
                    //case PayloadType.Pong:
                    //    var pongAtMillis = data.GetValueOrDefault<string, object>("pongAtMillis");
                    //    break;
                    //case PayloadType.Trader:
                    //    await _stompClient.UpdateTraderAsync(_trader.Id, data);
                    //    break;
                    case PayloadType.HistoryOrder:
                        await _stompClient.CreateOrUpdateHistoryOrderAsync(_trader.Id, json);
                        break;

                }

            }));


        }


    }
}