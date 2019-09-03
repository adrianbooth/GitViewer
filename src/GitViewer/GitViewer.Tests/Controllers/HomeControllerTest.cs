using System.Web.Mvc;
using GitViewer.Controllers;
using GitViewer.Domain.Logging;
using GitViewer.Services;
using Moq;
using NUnit.Framework;

namespace GitViewer.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            var mockGithubService = new Mock<IGithubUserService>();
            var mockLogger = new Mock<ILogger>();
            // Arrange
            HomeController controller = new HomeController(mockGithubService.Object, mockLogger.Object);

            // Act
            ViewResult result = controller.Index().GetAwaiter().GetResult() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }       
    }
}
