using GitViewer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitViewer.Models
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