using GitViewer.Domain.Logging;
using GitViewer.Models;
using GitViewer.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGithubUserService _githubUserService;
        private readonly ILogger _logger;

        public HomeController(IGithubUserService githubUserService, ILogger logger)
        {
            _githubUserService = githubUserService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(new GitUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(GitUserSearchViewModel model)
        {
            return View(new GitUserViewModel());
        }
    }
}