using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.Models
{
    public class Cart : BaseEntity
    {
        public required string UserId { get; set; }
        public AppUser User { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
