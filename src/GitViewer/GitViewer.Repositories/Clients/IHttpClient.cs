using System.Net.Http;
using System.Threading.Tasks;

namespace GitViewer.Repositories.Clients
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}