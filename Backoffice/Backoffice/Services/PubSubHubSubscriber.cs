namespace Backoffice.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PubSubHubSubscriber
    {
        private readonly HttpClient _httpClient;

        public PubSubHubSubscriber(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SubscribeAsync(string callbackUrl, string topicUrl, int leaseSeconds = 864000) // 10 días
        {
            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("hub.mode", "subscribe"),
            new KeyValuePair<string, string>("hub.callback", callbackUrl),
            new KeyValuePair<string, string>("hub.topic", topicUrl),
            new KeyValuePair<string, string>("hub.lease_seconds", leaseSeconds.ToString())
        });

            var response = await _httpClient.PostAsync("https://pubsubhubbub.appspot.com/subscribe", content);

            return response.IsSuccessStatusCode;
        }
    }

}
