using RestSharp;

namespace Dotrabot.Restful.TradingServer
{
    public class TradingServerFactory : AbstractFactory
    {
        public TradingServerFactory() : base("trading-servers")
        {
        }

        public async Task<TradingServerResult> createOrUpdate(TradingServerParam param)
        {
            RestRequest restRequest = new RestRequest()
            {
                RequestFormat = DataFormat.Json,
            };
            restRequest.AddBody(param);

            var response = await _restClient.ExecutePostAsync<TradingServerResult>(restRequest);
            if (response.IsSuccessful)
                return response.Data!;
            throw new Exception(response.Content);
        }
    }
}