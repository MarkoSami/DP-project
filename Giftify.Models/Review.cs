using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models
{
    public class Review : BaseEntity
    {
        public required string UserId { get; set; }
        public AppUser User { get; set; }
        public required int BookId { get; set; }
        public Book Book { get; set; }
        public required string Content { get; set; }

        [Range(0, 5)]
        public required int Rating { get; set; }
    }
}
