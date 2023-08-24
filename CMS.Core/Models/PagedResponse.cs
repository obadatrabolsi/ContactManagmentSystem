namespace Quivyo.Core.Models
{
    /// <summary>
    /// Represent an object for response that has pagination
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagedResponse<TEntity>
    {
        /// <summary>
        /// Represent the starting index of the pagination
        /// </summary>
        /// <example>0</example>
        public int PageIndex { get; set; }

        /// <summary>
        /// Represent the count of the items to retrieve
        /// </summary>
        /// <example>10</example>
        public int PageSize { get; set; }

        /// <summary>
        /// Represent the total count of the items exists into the DB
        /// </summary>
        /// <example>200</example>
        public int TotalCount { get; set; }

        /// <summary>
        /// Represent the total count of pages
        /// </summary>
        /// <example>20</example>
        public int TotalPages { get; set; } = 1;

        /// <summary>
        /// Represent the current page number
        /// </summary>
        /// <example>3</example>
        public int CurrentPage { get; set; } = 0;

        /// <summary>
        /// Represent the next page number
        /// </summary>
        /// <example>4</example>
        public int NextPage { get; set; } = 1;

        /// <summary>
        /// Represent the previous page number
        /// </summary>
        /// <example>2</example>
        public int PrevPage { get; set; } = 0;

        /// <summary>
        /// Represent whether the pagination is enabled or not
        /// </summary>
        /// <example>true</example>
        public bool PaginationEnabled { get; set; }

        /// <summary>
        /// Represent whether there is more pages to display or not
        /// </summary>
        /// <example>true</example>
        public bool HasMorePages { get; set; }

        /// <summary>
        /// Represent the list of data after the pagination
        /// </summary>
        public List<TEntity> List { get; set; }

        /// <summary>
        /// Crete instance of paged response
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="enablePagination"></param>
        /// <returns></returns>
        public static PagedResponse<TEntity> Create(IQueryable<TEntity> query, int pageIndex, int pageSize,
                bool enablePagination = true)
        {
            var totalCount = query.Count();

            if (pageIndex < 0)
                pageIndex = 0;

            if (pageSize < 1)
                pageSize = 10;

            if (enablePagination)
                query = query.Slice(pageIndex, pageSize);

            var resultList = query.ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var nextPage = pageIndex + 1 > totalPages ? totalPages : pageIndex + 1;
            var prevPage = pageIndex >= 1 ? pageIndex - 1 : 0;

            return new PagedResponse<TEntity>
            {
                TotalCount = totalCount,
                List = resultList,

                CurrentPage = enablePagination ? pageIndex : 0,
                TotalPages = enablePagination ? totalPages : 1,
                NextPage = enablePagination ? nextPage : 0,
                PrevPage = enablePagination ? prevPage : 0,
                PaginationEnabled = enablePagination,
                PageIndex = pageIndex,
                PageSize = pageSize,
                HasMorePages = enablePagination && pageIndex + 1 < totalPages,
            };
        }
        public static PagedResponse<TEntity> Create(List<TEntity> data, int totalCount, int pageIndex, int pageSize)
        {
            return new PagedResponse<TEntity>
            {
                TotalCount = totalCount,
                List = data,

                CurrentPage = pageIndex,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                NextPage = pageIndex++,
                PrevPage = pageIndex--,
                PaginationEnabled = true,
                PageIndex = pageIndex,
                PageSize = pageSize,
                HasMorePages = true,
            };
        }

        /// <summary>
        /// Crete instance of paged response
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="enablePagination"></param>
        /// <param name="actionWhenMapping"></param>
        /// <returns></returns>
        public static async Task<PagedResponse<TEntity>> CreateAsync(IQueryable<TEntity> query, int pageIndex, int pageSize,
                bool enablePagination = true, Func<TEntity, Task> actionWhenMapping = null)
        {
            if (pageIndex < 0)
                pageIndex = 0;

            if (pageSize < 1)
                pageSize = 10;

            var totalCount = 0;

            if (enablePagination)
            {
                totalCount =  await query.CountAsyncSafe();
                query = query.Slice(pageIndex, pageSize);
            }

            var resultList = await query.ToListAsyncSafe();
            totalCount = enablePagination ? totalCount : resultList.Count;

            if (actionWhenMapping != null)
                foreach (var item in resultList)
                    await actionWhenMapping(item);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var nextPage = pageIndex + 1 > totalPages ? totalPages : pageIndex + 1;
            var prevPage = pageIndex >= 1 ? pageIndex - 1 : 0;

            return new PagedResponse<TEntity>
            {
                TotalCount = totalCount,
                List = resultList,

                CurrentPage = enablePagination ? pageIndex : 0,
                TotalPages = enablePagination ? totalPages : 1,
                NextPage = enablePagination ? nextPage : 0,
                PrevPage = enablePagination ? prevPage : 0,
                PaginationEnabled = enablePagination,
                PageIndex = enablePagination ? pageIndex : 0,
                PageSize = enablePagination ? pageSize : totalCount,
                HasMorePages = enablePagination && pageIndex + 1 < totalPages,
            };
        }


    }
}
