using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yb.Dal.Base
{
    public partial class BaseDal<T> where T : class, new()
    {
        protected readonly SqlDbContext dbContext;
        protected readonly DbSet<T> entitys;

        public BaseDal(SqlDbContext sqlDbContext)
        {
            dbContext = sqlDbContext;
            entitys = dbContext.Set<T>();
        }

        virtual public bool Add(T t)
        {
            entitys.Add(t);
            return dbContext.SaveChanges() > 0;
        }

        virtual public async Task<bool> AddAsync(T t)
        {
            entitys.Add(t);
            return await dbContext.SaveChangesAsync() > 0;
        }

        virtual public bool AddRange(IEnumerable<T> ts)
        {
            entitys.AddRange(ts);
            return dbContext.SaveChanges() > 0;
        }

        virtual public async Task<bool> AddRangeAsync(IEnumerable<T> ts)
        {
            entitys.AddRange(ts);
            return await dbContext.SaveChangesAsync() > 0;
        }

        virtual public bool Delete(T t)
        {
            entitys.Remove(t);
            return dbContext.SaveChanges() > 0;
        }

        virtual public async Task<bool> DeleteAsync(T t)
        {
            entitys.Remove(t);
            return await dbContext.SaveChangesAsync() > 0;
        }

        virtual public bool DeleteRange(IEnumerable<T> ts)
        {
            entitys.RemoveRange(ts);
            return dbContext.SaveChanges() > 0;
        }

        virtual public async Task<bool> DeleteRangeAsync(IEnumerable<T> ts)
        {
            entitys.RemoveRange(ts);
            return await dbContext.SaveChangesAsync() > 0;
        }

        virtual public bool Update(T t)
        {
            entitys.Update(t);
            return dbContext.SaveChanges() > 0;
        }

        virtual public async Task<bool> UpdateAsync(T t)
        {
            entitys.Update(t);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRangeAsync(List<T> t, bool isSave = true)
        {
            entitys.UpdateRange(t);
            if (isSave)
                return await dbContext.SaveChangesAsync() > 0;
            return false;
        }

        virtual public bool Exists(Expression<Func<T, bool>> anyLambda)
        {
            return entitys.Any(anyLambda);
        }

        virtual public async Task<bool> ExistsAsync(Expression<Func<T, bool>> anyLambda)
        {
            return await entitys.AnyAsync(anyLambda);
        }

        virtual public T Find(object key)
        {
            return entitys.Find(key);
        }

        virtual public async Task<T> FindAsync(object key)
        {
            return await entitys.FindAsync(key);
        }

        virtual public T Find(Expression<Func<T, bool>> whereLambda)
        {
            return entitys.FirstOrDefault(whereLambda);
        }

        virtual public async Task<T> FindAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await entitys.FirstOrDefaultAsync(whereLambda);
        }

        virtual public IQueryable<T> Query()
        {
            return entitys;
        }

        virtual public IQueryable<T> Query(Expression<Func<T, bool>> whereLambda)
        {
            return entitys.Where(whereLambda);
        }

        virtual public IQueryable<T> Query<type>(Expression<Func<T, bool>> WhereLambda, Expression<Func<T, type>> OrderByLambda, bool isAsc)
        {
            var temp = entitys.AsQueryable();
            if (WhereLambda != null)
                temp = temp.Where(WhereLambda);
            if (OrderByLambda != null)
            {
                if (isAsc)
                    temp = temp.OrderBy(OrderByLambda);
                else
                    temp = temp.OrderByDescending(OrderByLambda);
            }
            return temp;
        }

        virtual public List<T> Query<type>(Expression<Func<T, bool>> WhereLambda, Expression<Func<T, type>> OrderByLambda,
            bool isAsc, int pageIndex, int pageSize, out int total)
        {
            var temp = entitys.AsQueryable();
            if (WhereLambda != null)
                temp = temp.Where(WhereLambda);
            if (OrderByLambda != null)
            {
                if (isAsc)
                    temp = temp.OrderBy(OrderByLambda);
                else
                    temp = temp.OrderByDescending(OrderByLambda);
            }
            total = temp.Count();
            if (total > 0)
                return temp.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            else
                return null;
        }

        virtual public bool SaveChanges()
        {
            return dbContext.SaveChanges() > 0;
        }
    }
}