using dotrabot.NET;
using Dotrabot.MT5.Schema;
using Dotrabot.StompClient;
using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client;
using Netina.Stomp.Client.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace Dotrabot.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MetaTrader _metaTrader = new MetaTrader();
        private DotrabotStompClient _stompClient = new DotrabotStompClient();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _metaTrader.ReceiveAsync(async (payload) =>
            {
                Debug.WriteLine(payload);
                var tradeMessage= JsonConvert.DeserializeObject<TradeMessage>(payload);
               await _stompClient.BroadcastTradeAsync(tradeMessage);
            });

            _stompClient.ConnectAsync(() =>
            {
                _stompClient.SubscribeAsync<String>(1, (message) =>
                {
                    Debug.WriteLine(message);
                });
            });
        }


    }
}