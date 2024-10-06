using System.ComponentModel.DataAnnotations;

namespace BlogSimplesMvc.UI.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(450, MinimumLength = 2, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
        public string Description { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.Now;

        public string AuthorId { get; set; }

        public Author Author { get; set; } 

        public ICollection<Comments> Comments { get; set; }
    }
}
