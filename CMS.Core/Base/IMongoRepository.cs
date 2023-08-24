using MongoDB.Bson;
using MongoDB.Driver;
using Quivyo.Core.Models;
using System.Linq.Expressions;

namespace YIT.Core.Base
{
    public interface IMongoRepository<TEntity> where TEntity : BaseEntity
    {
        Task InsertAsync(TEntity entity);

        Task InsertRangeAsync(List<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task DeleteAllAsync();

        Task DeleteAsync(Guid id);

        Task DeleteAsync(TEntity entity);

        Task DeleteManyAsync(List<Guid> ids);

        Task<TEntity> FirstOrDefaultAsync();

        Task<TEntity> FirstOrDefaultAsync(BsonDocument filter);

        Task<TEntity> FirstOrDefaultAsync(FilterDefinition<TEntity> filter);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        TEntity Find(Guid id);

        Task<TEntity> FindAsync(Guid id);

        Task<TEntity> MaxAsync(Expression<Func<TEntity, object>> expression);

        List<TEntity> ListAll();

        Task<List<TEntity>> ListAllAsync();

        List<TEntity> List(Expression<Func<TEntity, bool>> filter);

        Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter);

        Task<List<TEntity>> ListAsync(FilterDefinition<TEntity> filter);

        Task<List<TEntity>> ListInAsync<TField>(Expression<Func<TEntity, TField>> expression, List<TField> array);

        /// <summary>
        /// Retrieve a list of document paginated from the filtered data
        /// </summary>
        /// <param name="filter">Represent the query filter that would filter the result</param>
        /// <param name="sort">Represent the query sorting options that would sort the result</param>
        /// <param name="pageIndex">Represent the page index of the paginated result</param>
        /// <param name="pageSize">Represent the page size of the paginated result</param>
        /// <returns></returns>
        Task<PagedResponse<TEntity>> ListPaginatedAsync(FilterDefinition<TEntity> filter = default, SortDefinition<TEntity> sort = default,
            int pageIndex = 0, int pageSize = 10);

        /// <summary>
        /// Retrieve a list of document paginated from the filtered data
        /// </summary>
        /// <param name="selectExpression">Represent projection expression</param>
        /// <param name="filter">Represent the query filter that would filter the result</param>
        /// <param name="sort">Represent the query sorting options that would sort the result</param>
        /// <param name="pageIndex">Represent the page index of the paginated result</param>
        /// <param name="pageSize">Represent the page size of the paginated result</param>
        /// <returns></returns>
        Task<PagedResponse<TResponse>> ListPaginatedAsync<TResponse>(Func<TEntity, TResponse> selectExpression, FilterDefinition<TEntity> filter = default,
            SortDefinition<TEntity> sort = default, int pageIndex = 0, int pageSize = 10);

        Task<bool> AnyAsync();

        Task<bool> AnyAsync(FilterDefinition<TEntity> filter);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    }
}