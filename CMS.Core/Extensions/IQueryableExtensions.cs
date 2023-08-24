using Microsoft.EntityFrameworkCore;

namespace CMS.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static Task<List<TSource>> ToListAsyncSafe<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!(source is IAsyncEnumerable<TSource>))
                return Task.FromResult(source.ToList());

            return source.ToListAsync();
        }
        public static Task<int> CountAsyncSafe<TSource>(this IQueryable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!(source is IAsyncEnumerable<TSource>))
                return Task.FromResult(source.Count());

            return source.CountAsync();
        }
        public static IQueryable<T> Slice<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            return query.Skip(pageSize * pageIndex).Take(pageSize);
        }
        public static IQueryable<T> Slice<T>(this IQueryable<T> query, int? pageIndex, int? pageSize)
        {
            if (!pageIndex.HasValue || pageIndex < 0)
                pageIndex = 0;

            if (!pageSize.HasValue || pageSize < 0)
                pageSize = 10;

            return query.Skip(pageSize.Value * pageIndex.Value).Take(pageSize.Value);
        }
    }
}
