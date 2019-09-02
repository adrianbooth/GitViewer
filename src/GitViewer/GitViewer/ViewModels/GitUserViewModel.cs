using GitViewer.Domain.Models;

namespace GitViewer.ViewModels
{
    public class GitUserViewModel
    {
        public GithubUser User { get; set; }
        public bool HasData()
        {
            return !string.IsNullOrEmpty(User?.Username);
        }
    }
}