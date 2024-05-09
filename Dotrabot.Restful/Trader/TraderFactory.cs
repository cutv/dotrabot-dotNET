using Dotrabot.Restful.TradingServer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.Restful.Trader
{
    public class TraderFactory : AbstractFactory
    {
        public TraderFactory(IDotrabotClientConfig config) : base("traders", config)
        {
        }

        public async Task<TraderResult> findByMe()
        {
            var response = await _restClient.ExecuteGetAsync<Result<TraderResult>>(new RestRequest("me"));
            return ResponseWith<TraderResult>(response);
        }
    }


}
