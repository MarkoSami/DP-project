using Giftify.DAL.Repository.Interfaces;
using Giftify.DataAccess.Data;
using Giftify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.DAL.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Category item)
        {
            context.Categories.Update(item);
        }
    }
}
