using Dotrabot.StompClient;
using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client;
using Netina.Stomp.Client.Interfaces;
using Rananu.Shared;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace Dotrabot.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MetaTrader _metaTrader = new MetaTrader();
        IStompClient _stompClient = new Netina.Stomp.Client.StompClient("ws://localhost:8080/metatrader");
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _stompClient.OnConnect += async (sender, message) =>
                {
                    Debug.WriteLine("Connected");
                    await _stompClient.SubscribeAsync<object>(3, (message) =>
                    {
                        var payload = message.ToString();
                        if (payload != null)
                            _metaTrader.SendAsync(payload);
                    });
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
            Debug.WriteLine("Hello");
#if COPIER


#endif
#if MASTER
#endif

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
                        tradeMessage.traderId = 1;
                        await _stompClient.BroadcastTradeAsync(tradeMessage);
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
                        await _stompClient.UpdateTraderAsync(1, traderMessage);

                        break;
                }

            });


        }


    }
}