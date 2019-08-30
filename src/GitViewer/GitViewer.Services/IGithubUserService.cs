using GitViewer.Domain.Models;
using System.Threading.Tasks;

namespace GitViewer.Services
{
    public interface IGithubUserService
    {
        Task<GithubUser> GetGithubUser(string gitHandle);
    }
}