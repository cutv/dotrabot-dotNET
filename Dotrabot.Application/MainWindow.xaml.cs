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
        private ZeroMQ _zeroMQ = new ZeroMQ();

        IStompClient _stompClient;
        private TraderResult _trader;
        private readonly ConcurrentDictionary<String, byte> _dictionary = new ConcurrentDictionary<string, byte>();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public string Authorization => txtClientSecret.Text;

#if DEV
        public string BaseUrl => "http://localhost:8080";
#else
        public string BaseUrl => "http://14.225.207.213";
#endif

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> headers = new Dictionary<String, String>();
            headers.Add("Trader-Authorization", Authorization);
#if DEV

            _stompClient = new Netina.Stomp.Client.StompClient("ws://localhost:8080/metatrader", headers: headers);
#else
            _stompClient = new Netina.Stomp.Client.StompClient("ws://14.225.207.213/metatrader", headers: headers);
#endif
            ConfigurationFactory configurationFactory = new ConfigurationFactory(this);
            ConfigurationResult configuration = await configurationFactory.FindLatest();

            _stompClient.OnConnect += async (sender, message) =>
                {
                    _zeroMQ.EnableReceiving = true;
                    foreach (var key in _dictionary.Keys)
                    {
                        SubscribeTopicAsync(key);
                    }
                    Debug.WriteLine("Connected");
                    foreach (var item in configuration.middleware.subscribe_topics)
                    {
                        if (_dictionary.ContainsKey(item))
                            continue;
                        await _stompClient.SubscribeAsync(item, new Dictionary<String, String>(), (sender, message) =>
                            {
                                var payload = message.Body;
                                if (payload != null)
                                    _zeroMQ.SendAsync(payload);
                                Debug.WriteLine(payload);
                            });
                        _dictionary.TryAdd(item, 0);
                    }
                    
                  




                };
            _stompClient.OnError += (sender, message) =>
            {
                Debug.WriteLine("OnError" + message.ToString());
            };
            _stompClient.OnClose += (sender, message) =>
            {
                _zeroMQ.EnableReceiving = false;
                Debug.WriteLine("OnClose" + message.ToString());
            };
            _stompClient.OnReconnect += (sender, message) =>
            {
                Debug.WriteLine("OnReconnect " + message.ToString());
              
            };
            Dictionary<string, string> header = new Dictionary<String, String>();

            await _stompClient.ConnectAsync(header);

            _zeroMQ.OnReceived = (Action<string>)(async (message) =>
            {
                if (message.Contains("\"type\":\"initialize\""))
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
            });

            //_metaTrader.ReceiveAsync();


        }

        private async void SubscribeTopicAsync(String topic)
        {
            await _stompClient.SubscribeAsync(topic, new Dictionary<String, String>(), (sender, message) =>
            {
                var payload = message.Body;
                if (payload != null)
                {
                    JObject jObject = JObject.Parse(payload);
                    String server = (String)jObject.SelectToken("server");
                    String login = (String)jObject.SelectToken("login");
                    String topic = $"/servers/{server}/traders/{login}";
                    _zeroMQ.SendAsync(topic, payload);
                    Debug.WriteLine(payload);

                }


            });
            _dictionary.TryAdd(topic, 0);
        }


    }
}