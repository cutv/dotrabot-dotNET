using Dotrabot.Restful;
using Dotrabot.Restful.Configuration;
using Dotrabot.Restful.Trader;
using Dotrabot.StompClient;
using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client.Interfaces;
using Rananu.Shared;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using Websocket.Client;

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
            ConfigurationFactory configurationFactory = new ConfigurationFactory(this);
            ConfigurationResult configuration = await configurationFactory.FindLatest();
            TraderFactory traderFactory = new TraderFactory(this);
            _trader = await traderFactory.findByMe();
            tblName.Text = _trader.Name;

            _stompClient.OnConnect += async (sender, message) =>
                {
                    Debug.WriteLine("Connected");
                    await _stompClient.SubscribeMeAsync<String>(_trader.Id, async (message) =>
                    {
                        String payload = message;
                        if (payload != null)
                             _metaTrader.SendAsync(payload);
                        Debug.WriteLine(payload);
                    });

                    foreach (var item in configuration.middleware.subscribeTopics)
                    {
                        await _stompClient.SubscribeAsync("/ea", new Dictionary<String, String>(), (sender, message) =>
                            {
                                var payload = message.Body;
                                if (payload != null)
                                    _metaTrader.SendAsync(payload);
                                Debug.WriteLine(payload);
                            });
                    }


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
            Dictionary<string, string> headers = new Dictionary<String, String>();
            await _stompClient.ConnectAsync(headers);


            _metaTrader.ReceiveAsync((Action<string>)(async (message) =>
            {
                Debug.WriteLine(message);
                if (string.IsNullOrEmpty(message))
                    return;
                await _stompClient.SendAsync(configuration.middleware.topic, message);
            }));


        }


    }
}