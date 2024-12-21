using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models
{
    public class Book : BaseEntity
    {
        public new int? Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required double Price { get; set; }

        [Range(0,5)]
        public int Stock { get; set; } = 0;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        [Range(0,5)]
        public double Rating { get; set; } = 0;

        public required int CategoryId { get; set; }
        public Category? Category { get; set; }


    }
}
