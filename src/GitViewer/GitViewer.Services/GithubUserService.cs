using System;
using System.Threading.Tasks;
using GitViewer.Domain.Models;
using GitViewer.Repositories;

namespace GitViewer.Services
{
    public class GithubUserService : IGithubUserService
    {
        private readonly IGitHubDataRepository _repository;

        public GithubUserService(IGitHubDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<GithubUser> GetGithubUser(string gitHandle)
        {
            throw new NotImplementedException();
        }
    }
}
