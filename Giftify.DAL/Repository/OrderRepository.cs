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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
