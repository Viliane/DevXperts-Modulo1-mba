using System.ComponentModel.DataAnnotations;

namespace BlogSimplesAPI.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is not in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
        public string Password { get; set; }
    }
}
