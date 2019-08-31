using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitViewer.Domain.Logging;
using GitViewer.Domain.Models;
using GitViewer.Repositories;
using GitViewer.Repositories.Entities;

namespace GitViewer.Services
{
    public class GithubUserService : IGithubUserService
    {
        private readonly IGitHubDataRepository _repository;
        private readonly ILogger _logger;

        public GithubUserService(IGitHubDataRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<GithubUser> GetGithubUser(string gitHandle)
        {
            GithubUser githubUser = null;
            try
            {
                var user = await _repository.GetUserEntity(gitHandle);
                if(user == null)
                {
                    return githubUser;
                }

                var userRepositories = await _repository.GetRepositoryEntities(user.repos_url) ;
                    githubUser = UserBuilder(user, userRepositories);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, gitHandle);                
            }
            return githubUser;
        }

        private static GithubUser UserBuilder(UserEntity user, IEnumerable<RepositoryEntity> userRepositories)
        {
            return new GithubUser
            {
                Username = user.login,
                Avatar = user.avatar_url,
                Location = user.location,
                Repositories = userRepositories.
                OrderByDescending(e => e.stargazers_count)
                .Take(5)
                .Select(e =>
                new Repository
                {
                    Name = e.full_name,
                    StarGazers = e.stargazers_count
                })
            };
        }
    }
}
