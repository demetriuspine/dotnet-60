using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditorCategoryViewModel // apenas name e slug serão mandados plo postman/frontend
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Name length must be at least 3 and at most 40")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Slug is required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Slug length must be at least 3 and at most 20")]
        public string Slug { get; set; }
    }
}
