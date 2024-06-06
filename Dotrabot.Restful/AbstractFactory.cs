using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.Restful
{
    public abstract class AbstractFactory
    {
        protected RestClient _restClient;
        private IDotrabotClientConfig _config;
        public AbstractFactory(String resource, IDotrabotClientConfig config)
        {
            _config = config;
#if DEV
            var options = new RestClientOptions($"http://localhost:8080/api/{resource}")
            {
            };
#else
            var options = new RestClientOptions($"http://14.225.207.213/api/{resource}")
            {
            };
            _restClient = new RestClient(options);
            _restClient.AddDefaultHeader("Trader-Authorization", _config.Authorization);
        }
#endif
        public T ResponseWith<T>(RestResponse<Result<T>> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => response.Data.Data,
                HttpStatusCode.Unauthorized => throw new UnAuthorizedException(response.Data != null ? response.Data.Message : response.Content),
                _ => throw new Exception(response.Content)
            };
        }
    }
}
