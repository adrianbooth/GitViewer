using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitViewer.Domain.Logging;
using GitViewer.Repositories.Clients;
using GitViewer.Repositories.Entities;
using Newtonsoft.Json;

namespace GitViewer.Repositories
{
    public class GitHubAPIDataRepository : IGitHubDataRepository
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;

        public GitHubAPIDataRepository(IHttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<RepositoryEntity>> GetRepositoryEntities(string repositoryUrl)
        {
            try
            {
                var repositoryResponse = await _httpClient.GetAsync(repositoryUrl);
                return repositoryResponse.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<IEnumerable<RepositoryEntity>>(
                        await repositoryResponse.Content.ReadAsStringAsync())
                    : new List<RepositoryEntity>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, repositoryUrl);
                throw;
            }
        }

        public async Task<UserEntity> GetUserEntity(string gitHandle)
        {
            try
            {
                var userResponse = await _httpClient.GetAsync($"https://api.github.com/users/{gitHandle}");
                if (userResponse.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<UserEntity>(
                        await userResponse.Content.ReadAsStringAsync());
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, gitHandle);
                throw;
            }
        }
    }
}
