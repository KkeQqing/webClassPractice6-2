using System.Linq.Expressions;

namespace Yb.Bll.Base
{
    /// <summary>
    /// 分页结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPagedList<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 20;
        public int TotalCount { get; set; }

        public bool HasNextPage
        {
            get
            {
                return ((PageIndex - 1) * PageSize + (Data?.Count() ?? 0)) < TotalCount;
            }
        }

        public IEnumerable<T> Data { get; set; }

        public ApiPagedList(int pageIndex, int pageSize, int totalCount, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public ApiPagedList<TNew> New<TNew>(IEnumerable<TNew> ds)
        {
            return new ApiPagedList<TNew>(PageIndex, PageSize, TotalCount, ds);
        }

        public ApiPagedList<TNew> Select<TNew>(Expression<Func<T, TNew>> select)
        {
            var ds = Data.AsQueryable().Select(select).ToList();
            return new ApiPagedList<TNew>(PageIndex, PageSize, TotalCount, ds);
        }
    }

    public static class ApiPagedListExtension
    {
        public static ApiPagedList<T> ToApiPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 20;

            var total = source.Count();
            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;

            var data = source.Skip(skip).Take(take).ToList();

            return new ApiPagedList<T>(pageIndex, pageSize, total, data);
        }

        public static ApiPagedList<T> ToApiPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 20;

            var total = source.Count();
            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;

            var data = source.Skip(skip).Take(take).ToList();

            return new ApiPagedList<T>(pageIndex, pageSize, total, data);
        }

        public static ApiPagedList<T> ToApiPagedList<T>(this IQueryable<T> source, IPageQueryParam param = null)
        {
            var pageIndex = param?.pageIndex ?? 1;
            var pageSize = param?.pageSize ?? 20;
            return source.ToApiPagedList(pageIndex, pageSize);
        }

        public static ApiPagedList<T> ToApiPagedList<T>(this IEnumerable<T> source, IPageQueryParam param = null)
        {
            var pageIndex = param?.pageIndex ?? 1;
            var pageSize = param?.pageSize ?? 20;
            return source.ToApiPagedList(pageIndex, pageSize);
        }
    }
}