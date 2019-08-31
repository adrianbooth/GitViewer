using System.Net.Http;
using System.Threading.Tasks;

namespace GitViewer.Repositories.Clients
{
    public class BasicHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public BasicHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
    }
}
