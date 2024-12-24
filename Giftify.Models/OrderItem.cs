using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models
{
    public class OrderItem : BaseEntity 
    {
        public  int? OrderId { get; set; }
        public Order Order { get; set; }
        public required int BookId { get; set; }
        public Book Book { get; set; }
        public required int Quantity { get; set; }
        public required double Price { get; set; }
    }
}
