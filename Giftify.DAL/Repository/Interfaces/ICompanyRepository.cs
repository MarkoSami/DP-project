using Giftify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.DAL.Repository.Interfaces
{
    public interface ICompanyRepostiory : IRepository<Company>
    {
        void Update(Company item);
    }
}
