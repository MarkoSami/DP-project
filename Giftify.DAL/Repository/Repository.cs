using Giftify.DAL.Repository.Interfaces;
using Giftify.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Giftify.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext context;
        internal DbSet<T> dbSet;
        public Repository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T item)
        {

            dbSet.Add(item);
        }

        public T Get(Expression<Func<T, bool>> filter, string includeProps) //"Category,
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null, string includeProps = "")
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach(var prop in  includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            if (filter != null) { 
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public IQueryable<T> GetAsQueryable() {
            IQueryable<T> query = dbSet;
            return query;
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            dbSet.RemoveRange(items);
        }

        public void Update(T item)
        {
            dbSet.Update(item);
        }
    }
}
