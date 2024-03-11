using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot.Restful
{
    public abstract class AbstractFactory
    {
        protected RestClient _restClient;
        public AbstractFactory(String resource)
        {
            var options = new RestClientOptions($"http://dotrabot.cghue.com/api/{resource}")
            {
            };
            _restClient = new RestClient(options);
            _restClient.AddDefaultHeader("Authorization", "v1.public.eyJzdWIiOiIyNSIsImF1dCI6IlJPTEVfVVNFUiIsImV4cCI6IjIwMjQtMTItMjJUMTk6MDc6MDguMjI3Mjk3NVoifcmBtvIjlmV1t9he3U6iorbj6sderx7f7kS6RGqS5osEpDp495MIROHQKJdAh_06cBD3GFFqy0okVRQNXuPLXAEV4K5a4J-hYSdPNMazB0Ad5xpDGIfgWmI-yUV4HHzqYtHJTQBZseNK_tQQMwpMxgQLj1KqwHWIkK7e2EuLnKpNxi7ShywXKQdCdG5emFl2j_lWgAPoFq9uaO8oaPufxB3VMWiKq9MdOhMadM67xTzZcyOJNkq_ltmEPCasJmmbG9tMOPHg8NEjaZ6lOmKm4DcJgr_LOW2ya9WmJw_N4CskUSRjetWGETgOAIb9Nkoi_WwWNeru4tA28Ep9JauYzQQ.eyJraWQiOiI1MzZiMjQyZi1lYjhjLTRjNjItYmE4Zi1kN2E2M2MxNGNhZTkifQ");
        }
    }
}
