using GitViewer.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitViewer.Repositories
{
    public interface IGitHubDataRepository
    {
        Task<UserEntity> GetUserEntity (string gitHandle);
        Task<IEnumerable<RepositoryEntity>> GetRepositoryEntities(string repositoryUrl);
    }
}