using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GitViewer.Models
{
    public class GitUserSearchViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A Github Handle is required")]
        public string GitHandle { get; set; }
    }
}