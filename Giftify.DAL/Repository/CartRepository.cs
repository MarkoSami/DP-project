using Giftify.DAL.Repository.Interfaces;
using Giftify.DataAccess.Data;
using Giftify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.DAL.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }
    }
}
