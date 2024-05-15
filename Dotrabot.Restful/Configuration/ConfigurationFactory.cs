using Dotrabot.Restful.Trader;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.Restful.Configuration
{
   
    public class ConfigurationFactory : AbstractFactory
    {
        public ConfigurationFactory(IDotrabotClientConfig config) : base("configurations", config)
        {
        }

        public async Task<ConfigurationResult> FindLatest()
        {
            var response = await _restClient.ExecuteGetAsync<Result<ConfigurationResult>>(new RestRequest("latest"));
            return ResponseWith<ConfigurationResult>(response);
        }
    }
}
