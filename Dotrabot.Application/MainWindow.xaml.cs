using Dotrabot.Restful;
using Dotrabot.Restful.Configuration;
using Dotrabot.Restful.Trader;
using Dotrabot.StompClient;
using Dotrabot.StompClient.Schema;
using Netina.Stomp.Client.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rananu.Shared;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json.Nodes;
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

        IStompClient _stompClient;
        //IStompClient _stompClient = new Netina.Stomp.Client.StompClient("ws://14.225.207.213/metatrader");
        private readonly ConcurrentDictionary<String, byte> _dictionary=new ConcurrentDictionary<string, byte>();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public string Authorization => txtClientSecret.Text;

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> headers = new Dictionary<String, String>();
            headers.Add("Trader-Authorization", Authorization);
            _stompClient = new Netina.Stomp.Client.StompClient("ws://localhost:8080/metatrader", headers: headers);
            StompClientExtension.Authorization = Authorization;
            ConfigurationFactory configurationFactory = new ConfigurationFactory(this);
            ConfigurationResult configuration = await configurationFactory.FindLatest();
            //TraderFactory traderFactory = new TraderFactory(this);
            //_trader = await traderFactory.findByMe();
           // tblName.Text = _trader.Name;

            _stompClient.OnConnect += async (sender, message) =>
                {
                    Debug.WriteLine("Connected");
                    foreach (var item in configuration.middleware.subscribe_topics)
                    {
                        await _stompClient.SubscribeAsync(item, new Dictionary<String, String>(), (sender, message) =>
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
            Dictionary<string, string> header = new Dictionary<String, String>();

            await _stompClient.ConnectAsync(header);


            _metaTrader.ReceiveAsync((Action<string>)(async (message) =>
            {
                if (message.StartsWith("{\"type\":\"initialize\""))
                {
                    JObject jObject = JObject.Parse(message);
                    String json = (String)jObject.SelectToken("data");
                    jObject = JObject.Parse(json);
                    String server = (String)jObject.SelectToken("server");
                    String login = (String)jObject.SelectToken("login");
                    String topic = $"/servers/{server}/traders/{login}";

                    if (!String.IsNullOrEmpty(server)
                        && !String.IsNullOrEmpty(login)
                        && !_dictionary.ContainsKey(topic)
                        )
                        SubscribeTopicAsync(topic);
                }
                Debug.WriteLine(message);
                if (string.IsNullOrEmpty(message))
                    return;
                await _stompClient.SendAsync(configuration.middleware.Topic, message);
            }));


        }

        private async void SubscribeTopicAsync(String topic)
        {
            await _stompClient.SubscribeAsync(topic, new Dictionary<String, String>(), (sender, message) =>
            {
                var payload = message.Body;
                if (payload != null)
                    _metaTrader.SendAsync(payload);
                Debug.WriteLine(payload);
            });
            _dictionary.TryAdd(topic, 0);
        }


    }
}