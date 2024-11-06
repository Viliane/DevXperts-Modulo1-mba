using System.ComponentModel.DataAnnotations;

namespace BlogSimplesAPI.Models
{
    public class CommentView
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(450, MinimumLength = 2, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string AuthorId { get; set; }
    }
}
