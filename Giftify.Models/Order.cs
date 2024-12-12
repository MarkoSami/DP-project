using Giftify.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models
{
    public class Order : BaseEntity 
    {
        public required string UserId { get; set; }
        public AppUser User { get; set; }
        public required double TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus Status{ get; set; } = OrderStatus.Pending;
    }
}
