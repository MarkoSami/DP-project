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
    public class CompanyRepostiory : Repository<Company>, ICompanyRepostiory
    {
        private readonly AppDbContext context;

        public CompanyRepostiory(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public void Update(Company item)
        {
            context.Companies.Update(item);
        }
    }
}
