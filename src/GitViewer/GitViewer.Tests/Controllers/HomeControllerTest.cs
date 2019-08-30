using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitViewer.Controllers;
using Moq;
using GitViewer.Services;

namespace GitViewer.Tests.Unit.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var mockGithubService = new Mock<IGithubUserService>();
            // Arrange
            HomeController controller = new HomeController(mockGithubService.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }       
    }
}
