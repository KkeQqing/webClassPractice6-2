using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yb.Dal.Base;

namespace Yb.Bll.Base
{
    public class BaseBll<T> where T : class, new()
    {
        private readonly BaseDal<T> baseDal;

        public BaseBll(BaseDal<T> _dal)
        {
            this.baseDal = _dal;
        }

        virtual public bool Add(T t)
        {
            return baseDal.Add(t);
        }

        virtual public async Task<bool> AddAsync(T t)
        {
            return await baseDal.AddAsync(t);
        }

        virtual public bool AddRange(IEnumerable<T> ts)
        {
            return baseDal.AddRange(ts);
        }

        virtual public async Task<bool> AddRangeAsync(IEnumerable<T> ts)
        {
            return await baseDal.AddRangeAsync(ts);
        }

        public bool Delete(int Id)
        {
            T t = baseDal.Find(Id);
            return baseDal.Delete(t);
        }

        virtual public bool Delete(T t)
        {
            return baseDal.Delete(t);
        }

        virtual public async Task<bool> DeleteAsync(T t)
        {
            return await baseDal.DeleteAsync(t);
        }

        virtual public bool DeleteRange(IEnumerable<T> ts)
        {
            return baseDal.DeleteRange(ts);
        }

        virtual public async Task<bool> DeleteRangeAsync(IEnumerable<T> ts)
        {
            return await baseDal.DeleteRangeAsync(ts);
        }

        virtual public bool Update(T t)
        {
            return baseDal.Update(t);
        }

        virtual public async Task<bool> UpdateAsync(T t)
        {
            return await baseDal.UpdateAsync(t);
        }

        public async Task<bool> UpdateRangeAsync(List<T> ts)
        {
            return await baseDal.UpdateRangeAsync(ts, true);
        }

        virtual public bool Exists(Expression<Func<T, bool>> anyLambda)
        {
            return baseDal.Exists(anyLambda);
        }

        virtual public async Task<bool> ExistsAsync(Expression<Func<T, bool>> anyLambda)
        {
            return await baseDal.ExistsAsync(anyLambda);
        }

        virtual public T Find(object key)
        {
            return baseDal.Find(key);
        }

        virtual public async Task<T> FindAsync(object key)
        {
            return await baseDal.FindAsync(key);
        }

        virtual public T Find(Expression<Func<T, bool>> whereLambda)
        {
            return baseDal.Find(whereLambda);
        }

        virtual public async Task<T> FindAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await baseDal.FindAsync(whereLambda);
        }

        virtual public IQueryable<T> Query(Expression<Func<T, bool>> whereLambda)
        {
            return baseDal.Query(whereLambda);
        }

        virtual public IQueryable<T> Query()
        {
            return baseDal.Query();
        }

        virtual public bool SaveChanges()
        {
            return baseDal.SaveChanges();
        }
    }
}