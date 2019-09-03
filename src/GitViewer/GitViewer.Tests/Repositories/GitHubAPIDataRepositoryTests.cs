using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitViewer.Repositories.Clients;
using Moq;
using GitViewer.Domain.Logging;
using System.Net.Http;
using System.Net;
using GitViewer.Repositories;

namespace GitViewer.Tests.Repositories
{
    [TestFixture]
    public class GitHubAPIDataRepositoryTests
    {
        private readonly string repositoryResponseJsonFormatString = "[{{\"name\": \"{0}\",  \"stargazers_count\": {1} }}]";
        private readonly string userResponseJsonFormatString = "{{ \"login\": \"{0}\",  \"avatar_url\": \"{1}\",  \"location\": \"{2}\" }}";

        [TestCase("adrian", "someurl", "somelocation")]
        [TestCase("peter", "anotherurl", "anotherlocation")]
        public async Task GetUserEntity_ReturnsCorrectObject_WhenApiIsOK(string name, string avatar, string location)
        {
            var httpResponseString = string.Format(userResponseJsonFormatString, name, avatar, location);
            var mockHttpClient = new Mock<IHttpClient>();
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            mockHttpClient.Setup(e => e.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(httpResponseString, Encoding.UTF8, "application/json")
                }));

            var sut = new GitHubAPIDataRepository(mockHttpClient.Object, mockLogger.Object);
            var response = await sut.GetUserEntity("gitHandle");

            Assert.That(response.login == name);
            Assert.That(response.avatar_url == avatar);
            Assert.That(response.location == location);
        }

        [Test]
        public async Task GetUserEntity_ReturnsNull_WhenApiIsNonSuccess()
        {
            var mockHttpClient = new Mock<IHttpClient>();
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            mockHttpClient.Setup(e => e.GetAsync(It.IsAny<string>())).Returns(
                Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));

            var sut = new GitHubAPIDataRepository(mockHttpClient.Object, mockLogger.Object);
            var response = await sut.GetUserEntity("gitHandle");

            Assert.IsNull(response);
        }

        [TestCase("a log message")]
        [TestCase("different log message")]
        public async Task GetUserEntity_LogsErrors_WhenclientThrowsExceptions(string errorMessage)
        {
            var mockHttpClient = new Mock<IHttpClient>();
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            mockLogger.Setup(e => e.Error(It.IsAny<Exception>(), "gitHandle")).Verifiable();
            mockHttpClient.Setup(e => e.GetAsync(It.IsAny<string>())).Throws(new Exception(errorMessage));

            var sut = new GitHubAPIDataRepository(mockHttpClient.Object, mockLogger.Object);
            Assert.ThrowsAsync<Exception>(async () => await sut.GetUserEntity("gitHandle"));
            mockLogger.Verify(e => e.Error(It.IsAny<Exception>(), "gitHandle"), Times.Once);
        }

        [TestCase("adrian", 2)]
        [TestCase("peter", 3)]
        public async Task GetRepositoryEntities_ReturnsCorrectObject_WhenApiIsOK(string name, int stargazers)
        {
            var httpResponseString = string.Format(repositoryResponseJsonFormatString, name, stargazers);
            var mockHttpClient = new Mock<IHttpClient>();
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            mockHttpClient.Setup(e => e.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(httpResponseString, Encoding.UTF8, "application/json")
                }));

            var sut = new GitHubAPIDataRepository(mockHttpClient.Object, mockLogger.Object);
            var response = await sut.GetRepositoryEntities("repositoryUrl");
            var returnedRepository = response.FirstOrDefault();

            Assert.That(returnedRepository.name == name);
            Assert.That(returnedRepository.stargazers_count == stargazers);
        }

        [Test]
        public async Task GetRepositoryEntities_ReturnsEmptyCollection_WhenApiIsNonSuccess()
        {
            var mockHttpClient = new Mock<IHttpClient>();
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            mockHttpClient.Setup(e => e.GetAsync(It.IsAny<string>())).Returns(
                Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));

            var sut = new GitHubAPIDataRepository(mockHttpClient.Object, mockLogger.Object);
            var response = await sut.GetRepositoryEntities("repoUrl");

            Assert.That(response.Count() == 0);
        }

        [TestCase("a log message")]
        [TestCase("different log message")]
        public async Task GetRepositoryEntities_LogsErrors_WhenclientThrowsExceptions(string errorMessage)
        {
            var mockHttpClient = new Mock<IHttpClient>();
            var mockLogger = new Mock<ILogger>(MockBehavior.Strict);
            mockHttpClient.Setup(e => e.GetAsync(It.IsAny<string>())).Throws(new Exception(errorMessage));
            mockLogger.Setup(e => e.Error(It.IsAny<Exception>(), "repoUrl")).Verifiable();

            var sut = new GitHubAPIDataRepository(mockHttpClient.Object, mockLogger.Object);
            Assert.ThrowsAsync<Exception>(async () => await sut.GetRepositoryEntities("repoUrl"));
            mockLogger.Verify(e => e.Error(It.IsAny<Exception>(), "repoUrl"), Times.Once);
        }
    }
}
