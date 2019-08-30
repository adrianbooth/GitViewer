using System.Collections.Generic;

namespace GitViewer.Domain.Models
{
    public class GithubUser
    {
        public string Username { get; set; }
        public string Location{ get; set; }
        public string Avatar{ get; set; }

        public IEnumerable<Repository> Repositories { get; set; }
    }
}
