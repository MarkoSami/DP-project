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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public ICategoryRepository Category { get; }
        public IBookRepository Book{ get; }
        public IOrderRepository Order{ get; }

        public ICartRepository Cart { get; }




        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            Category = new CategoryRepository(context);
            Book = new BookRepository(context);
            Cart = new CartRepository(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
