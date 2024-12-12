using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models
{
    public class CartItem : BaseEntity
    {
        public required int CartId { get; set; }
        public Cart Cart { get; set; }
        public required int BookId { get; set; }
        public Book Book { get; set; }
        public required int Quantity { get; set; }
    }
}
