using CMS.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CMS.Core.Base
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

        Task<PagedResponse<TEntity>> ListPaginatedAsync(PagedRequest options);

        Task<PagedResponse<TResponse>> ListPaginatedAsync<TResponse>(Func<TEntity, TResponse> selectExpression, PagedRequest options);

        Task<bool> AnyAsync();

        Task<bool> AnyAsync(FilterDefinition<TEntity> filter);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    }
}