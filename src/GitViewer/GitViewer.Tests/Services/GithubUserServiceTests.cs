using GitViewer.Domain.Logging;
using GitViewer.Domain.Models;
using GitViewer.Repositories;
using GitViewer.Repositories.Entities;
using GitViewer.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitViewer.Tests.Services
{
    [TestFixture]
    public class GithubUserServiceTests
    {
        private UserEntity GetTestUserEntity()
        {
            return new UserEntity
            {
                avatar_url = "valid-url",
                login = "adrianbooth",
                location = "Sunderland, UK"
            };
        }

        private IEnumerable<RepositoryEntity> GetRepositoryEntities()
        {
            var repos = new List<RepositoryEntity>();

            for (int i = 0; i < 10; i++)
            {
                repos.Add(
                    new RepositoryEntity
                    {
                        name = $"name-{i}",
                        stargazers_count = i
                    });
            }

            return repos;
        }

        [Test]
        public async Task GetGithubUser_ReturnsCorrectlyMappedObject_WhenRepositoryReturnsEntities()
        {
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            var mockRepository = new Mock<IGitHubDataRepository>();

            var repos = GetRepositoryEntities();
            var user = GetTestUserEntity();

            mockRepository.Setup(e => e.GetUserEntity(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            mockRepository.Setup(e => e.GetRepositoryEntities(It.IsAny<string>()))
                .Returns(Task.FromResult(repos));

            var sut = new GithubUserService(mockRepository.Object, mockLogger.Object);
            var response = await sut.GetGithubUser("gitHandle");

            // test mapping
            Assert.That(response.Username == user.login);
            Assert.That(response.Avatar == user.avatar_url);
            Assert.That(response.Location == user.location);
            // limit stargazers to top 5
            Assert.That(response.Repositories.Count() == 5);
            // Make sure that the top 5 were took
            Assert.That(response.Repositories.Min(e => e.StarGazers) == 5);

        }

        [Test]
        public async Task GetGithubUser_ReturnsNull_WhenUserResponseIsNull()
        {
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            var mockRepository = new Mock<IGitHubDataRepository>();

            var sut = new GithubUserService(mockRepository.Object, mockLogger.Object);
            var response = await sut.GetGithubUser("gitHandle");

            Assert.IsNull(response);
        }

        [Test]
        public async Task GetGithubUser_LogsErrorsAndDoesNotThrow_WhenRepositoryThrowsExceptions()
        {
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            var mockRepository = new Mock<IGitHubDataRepository>();
            mockRepository.Setup(e=>e.GetUserEntity(It.IsAny<string>())).Throws(new Exception());
            mockLogger.Setup(e => e.Error(It.IsAny<Exception>(), "gitHandle")).Verifiable();

            var sut = new GithubUserService(mockRepository.Object, mockLogger.Object);
            var response = await sut.GetGithubUser("gitHandle");

            Assert.DoesNotThrowAsync(async () => await sut.GetGithubUser("gitHandle"));
            mockLogger.Verify(e => e.Error(It.IsAny<Exception>(), "gitHandle"));
        }

    }
}
