using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models.ViewModels
{
    public class ReviewVM
    {

        [Required]
        public string UserId { get; set; } // Foreign key for the user

        [Required]
        public int BookId { get; set; } // Foreign key for the book
        [Required]
        [Range(0, 5)]
        [Display(Name = "Rating")]
        public int Rating { get; set; } // Rating value

        [Required]
        [Display(Name = "Review Content")]
        public string Content { get; set; } // Review content

    }
}
