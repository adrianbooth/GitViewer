using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GitViewer.Controllers;
using GitViewer.Domain.Logging;
using GitViewer.Domain.Models;
using GitViewer.Services;
using GitViewer.ViewModels;
using Moq;
using NUnit.Framework;

namespace GitViewer.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private GithubUser GetGithubUser(string username)
        {
            return new GithubUser
            {
                Username = username,
                Location = "Sunderland",
                Avatar = "avatar_url",
                Repositories = new List<Repository>
            {
                 new Repository
                 {
                     Name = "repoName",
                     StarGazers = 2
                 }
            }
            };
        }
        [Test]
        public async Task Index_Get_ReturnsViewWithCorrectModel_WhenRequestValid()
        {
            var mockGithubService = new Mock<IGithubUserService>();
            var mockLogger = new Mock<ILogger>();
            var sut = new HomeController(mockGithubService.Object, mockLogger.Object);

            var result = await sut.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<GitUserViewModel>(result.Model);
        }

        [TestCase("adrianbooth")]
        [TestCase("robconery")]
        public async Task Index_Post_ReturnsViewWithCorrectModel_WhenRequestValid(string gitHandle)
        {
            var mockGithubService = new Mock<IGithubUserService>();
            var mockLogger = new Mock<ILogger>();
            mockGithubService.Setup(e => e.GetGithubUser(It.IsAny<string>()))
                .Returns(Task.FromResult(GetGithubUser(gitHandle)));
            var sut = new HomeController(mockGithubService.Object, mockLogger.Object);

            var result = await sut.Index(new GitUserSearchViewModel
            {
                GitHandle = "validHandle"
            }) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<GitUserViewModel>(result.Model);
            Assert.AreEqual(((GitUserViewModel)result.Model).User.Username, gitHandle);
            Assert.AreEqual(((GitUserViewModel)result.Model).User.Repositories.Count(), 1);
            Assert.AreEqual(((GitUserViewModel)result.Model).User.Location, "Sunderland");
        }

        [Test]
        public async Task Index_Post_ReturnsViewWithEmptyModel_OnBadRequest()
        {
            var mockGithubService = new Mock<IGithubUserService>();
            var mockLogger = new Mock<ILogger>();

            var sut = new HomeController(mockGithubService.Object, mockLogger.Object);

            var result = await sut.Index(new GitUserSearchViewModel()) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<GitUserViewModel>(result.Model);
            Assert.IsNull(((GitUserViewModel)result.Model).User);
        }
    }
}
