using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.DAL.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> filter, string includeProps="");

        List<T> GetAll(string includeProps = "");

        void Add(T item);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> items);



    }
}
