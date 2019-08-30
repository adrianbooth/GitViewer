using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitViewer.Repositories.Entities;

namespace GitViewer.Repositories
{
    public class GitHubAPIDataRepository : IGitHubDataRepository
    {
        private readonly HttpClient _httpClient;
        public GitHubAPIDataRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RepositoryEntity>> GetRepositoryEntities(string repositoryUrl)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserEntity(string gitHandle)
        {
            throw new NotImplementedException();
        }
    }
}
