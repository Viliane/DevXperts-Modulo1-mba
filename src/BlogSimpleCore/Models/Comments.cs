using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSimpleCore.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(450, MinimumLength = 2, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
        public string Description { get; set; }
    }
}
