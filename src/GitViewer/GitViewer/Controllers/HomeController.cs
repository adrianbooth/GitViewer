using GitViewer.Services;
using System.Web.Mvc;

namespace GitViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGithubUserService _githubUserService;

        public HomeController(IGithubUserService githubUserService)
        {
            _githubUserService = githubUserService;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}