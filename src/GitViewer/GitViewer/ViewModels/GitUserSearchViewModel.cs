using System.ComponentModel.DataAnnotations;

namespace GitViewer.ViewModels
{
    public class GitUserSearchViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A Github Handle is required")]
        public string GitHandle { get; set; }
    }
}