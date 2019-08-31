using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitViewer.Controllers;
using Moq;
using GitViewer.Services;
using GitViewer.Domain.Logging;

namespace GitViewer.Tests.Unit.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
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
