using Netina.Stomp.Client;
using Netina.Stomp.Client.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace dotrabot.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MetaTrader _metaTrader;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded1; 
        }

        private void MainWindow_Loaded1(object sender, RoutedEventArgs e)
        {
            _metaTrader = new MetaTrader("@tcp://localhost:5555");
            _metaTrader.ReceiveAsync((payload) =>
            {
                Debug.WriteLine(payload);
            });


        }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string json = File.ReadAllText("trade.json");
            log.AppendText("ReadAllText:" + json + Environment.NewLine);
            IStompClient client = new StompClient("ws://localhost:8080/metatrader");
            var headers = new Dictionary<string, string>();
            await client.ConnectAsync(headers);
            log.AppendText("Connected" + Environment.NewLine);

#if MASTER
            dispatcherTimer.Tick += async (sender,e)=> {

                    var body = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);
                await client.SendAsync(body, "/masters/broadcast", body);

        };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

#endif
#if COPIER
            log.AppendText("subscribe /copiers/3" + Environment.NewLine);
            await client.SubscribeAsync<object>("/copiers/3", new Dictionary<string, string>(), (sender, dto) =>
            {
                Dispatcher.BeginInvoke((() =>
                {
                    log.AppendText(dto.ToString() + Environment.NewLine);
                }));
            });
#endif





        }

     
    }
}