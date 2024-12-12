using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<Book> Books { get; set; }
    }
}
