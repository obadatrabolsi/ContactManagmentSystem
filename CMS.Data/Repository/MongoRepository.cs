using CMS.Core.Base;
using CMS.Core.Extensions;
using CMS.Core.Models;
using CMS.Core.Settings;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CMS.Data.Repository
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : BaseEntity
    {
        #region Props & Ctor

        private readonly IMongoDatabase db;
        private readonly string _collectionName;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IMongoCollection<TEntity> Collection => GetCollection();
        public IQueryable<TEntity> Table => GetCollection().AsQueryable();

        public MongoRepository(MongoDbSettings mongoDbConfig,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var dbName = mongoDbConfig.Name;

            var client = new MongoClient();

            // this will create the database if not exists or retrieve the database if exists
            db = client.GetDatabase(dbName);
            _collectionName = typeof(TEntity).Name;
        }

        #endregion Props & Ctor

        #region Methods

        /// <summary>
        /// Insert new document into the collection
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Insert a list of documents into the collection
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task InsertRangeAsync(List<TEntity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }

        /// <summary>
        /// Update single document. This will update the whole entity fields.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            await Collection.ReplaceOneAsync(t => t.Id == entity.Id, entity);
        }

        /// <summary>
        /// Delete all documents into the collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAllAsync()
        {
            await Collection.DeleteManyAsync(FilterDefinition<TEntity>.Empty);
        }

        /// <summary>
        /// Delete single document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var result = await Collection.DeleteOneAsync(t => t.Id == id);
        }

        /// <summary>
        /// Delete single document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            await Collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        /// <summary>
        /// Delete a list of documents by documents ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteManyAsync(List<Guid> ids)
        {
            await Collection.DeleteManyAsync(t => ids.Contains(t.Id));
        }

        /// <summary>
        /// Retrieve the first document into the collection
        /// </summary>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Collection.Find(new BsonDocument())
                   .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieve the first document from the filtered data
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(BsonDocument filter)
        {
            return await Collection.Find(filter)
                   .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieve the first document from the filtered data
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(FilterDefinition<TEntity> filter)
        {
            return await Collection.Find(filter)
                   .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieve single document from the filtered data
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Collection.Find(filter)
                   .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieve single document using document id synchronously
        /// </summary>
        /// <returns></returns>
        public TEntity Find(Guid id)
        {
            return Collection.Find(t => t.Id == id)
                   .FirstOrDefault();
        }

        /// <summary>
        /// Retrieve single document using document id
        /// </summary>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(Guid id)
        {
            return await Collection.Find(t => t.Id == id)
                   .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieve single document using the max value for the given field
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<TEntity> MaxAsync(Expression<Func<TEntity, object>> expression)
        {
            var options = new FindOptions<TEntity, TEntity>
            {
                Limit = 1,
                Sort = Builders<TEntity>.Sort.Descending(expression)
            };

            var find = await Collection.Find(t => true).SortByDescending(expression).Limit(1).FirstOrDefaultAsync();

            return find;
        }

        /// <summary>
        /// Retrieve all documents from the collection synchronously
        /// </summary>
        /// <returns></returns>
        public List<TEntity> ListAll()
        {
            return Collection.Find(new BsonDocument())
                   .ToList();
        }

        /// <summary>
        /// Retrieve all documents from the collection
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> ListAllAsync()
        {
            return await Collection.Find(new BsonDocument())
                   .ToListAsync();
        }

        /// <summary>
        /// Retrieve a list of document from the filtered data synchronously
        /// </summary>
        /// <returns></returns>
        public List<TEntity> List(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter)
                   .ToList();
        }

        /// <summary>
        /// Retrieve a list of document from the filtered data
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Collection.Find(filter)
                   .ToListAsync();
        }

        /// <summary>
        /// Retrieve a list of document from the filtered data
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> ListAsync(FilterDefinition<TEntity> filter)
        {
            return await Collection.Find(filter)
                   .ToListAsync();
        }

        /// <summary>
        /// Retrieve a list of document from the filtered data that matches any of the given array
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> ListInAsync<TField>(Expression<Func<TEntity, TField>> expression, List<TField> array)
        {
            var filter = Builders<TEntity>.Filter.AnyIn("_id", array);

            return await (await Collection.FindAsync(filter))
                   .ToListAsync();
        }

        /// <summary>
        /// Retrieve a list of document paginated from the filtered data
        /// </summary>
        /// <param name="filter">Represent the query filter that would filter the result</param>
        /// <param name="sort">Represent the query sorting options that would sort the result</param>
        /// <param name="pageIndex">Represent the page index of the paginated result</param>
        /// <param name="pageSize">Represent the page size of the paginated result</param>
        /// <returns></returns>
        public async Task<PagedResponse<TEntity>> ListPaginatedAsync(PagedRequest options)
        {
            var filter = PrepareFilter(options);
            var sort = PrepareSorting(options);

            var pageIndex = options.PageIndex;
            var pageSize = options.PageSize;


            if (pageIndex < 0)
                pageIndex = 0;

            if (pageSize < 1)
                pageSize = 10;

            var countFacet = GetCountFacet();

            var dataFacet = AggregateFacet.Create("data",
                    PipelineDefinition<TEntity, TEntity>.Create(new[]
                    {
                        PipelineStageDefinitionBuilder.Sort(sort),
                        PipelineStageDefinitionBuilder.Skip<TEntity>(pageIndex * pageSize),
                        PipelineStageDefinitionBuilder.Limit<TEntity>(pageSize),
                    }));

            var aggregation = await Collection.Aggregate()
                                              .Match(filter)
                                              .Facet(countFacet, dataFacet)
                                              .ToListAsync();

            var count = (int)(aggregation.First()
                                    .Facets.First(x => x.Name == "count")
                                    .Output<AggregateCountResult>()
                                    ?.FirstOrDefault()
                                    ?.Count ?? 0);

            var data = aggregation.First()
                           .Facets.First(x => x.Name == "data")
                           .Output<TEntity>();

            return PagedResponse<TEntity>.Create(data.ToList(), count, pageIndex, pageSize);
        }

        /// <summary>
        /// Retrieve a list of document paginated from the filtered data
        /// </summary>
        /// <param name="selectExpression">Represent projection expression</param>
        /// <param name="filter">Represent the query filter that would filter the result</param>
        /// <param name="sort">Represent the query sorting options that would sort the result</param>
        /// <param name="pageIndex">Represent the page index of the paginated result</param>
        /// <param name="pageSize">Represent the page size of the paginated result</param>
        /// <returns></returns>
        public async Task<PagedResponse<TResponse>> ListPaginatedAsync<TResponse>(Func<TEntity, TResponse> selectExpression, PagedRequest options)
        {
            var filter = PrepareFilter(options);
            var sort = PrepareSorting(options);

            var pageIndex = options.PageIndex;
            var pageSize = options.PageSize;

            if (pageIndex < 0)
                pageIndex = 0;

            if (pageSize < 1)
                pageSize = 10;

            var countFacet = GetCountFacet();

            var dataFacet = AggregateFacet.Create("data",
                    PipelineDefinition<TEntity, TEntity>.Create(new[]
                    {
                        PipelineStageDefinitionBuilder.Sort(sort),
                        PipelineStageDefinitionBuilder.Skip<TEntity>(pageIndex * pageSize),
                        PipelineStageDefinitionBuilder.Limit<TEntity>(pageSize),
                    }));

            var aggregation = await Collection.Aggregate()
                                              .Match(filter)
                                              .Facet(countFacet, dataFacet)
                                              .ToListAsync();

            var count = (int)(aggregation.First()
                                    .Facets.First(x => x.Name == "count")
                                    .Output<AggregateCountResult>()
                                    ?.FirstOrDefault()
                                    ?.Count ?? 0);

            var data = aggregation.First()
                           .Facets.First(x => x.Name == "data")
                           .Output<TEntity>();

            return PagedResponse<TResponse>.Create(data.Select(selectExpression).ToList(), count, pageIndex, pageSize);
        }

        /// <summary>
        /// Return whether there is any data into the collection
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AnyAsync()
        {
            return await Collection.Find(new BsonDocument())
                   .AnyAsync();
        }

        /// <summary>
        /// Return whether there is any data from the filtered data
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AnyAsync(FilterDefinition<TEntity> filter)
        {
            return await Collection.Find(filter)
                   .AnyAsync();
        }

        /// <summary>
        /// Return whether there is any data from the filtered data
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Collection.Find(filter)
                   .AnyAsync();
        }

        #endregion Methods

        #region Utilities

        private AggregateFacet<TEntity, AggregateCountResult> GetCountFacet()
        {
            var countFacet = AggregateFacet.Create("count",
            PipelineDefinition<TEntity, AggregateCountResult>.Create(new[]
            {
                PipelineStageDefinitionBuilder.Count<TEntity>()
            }));

            return countFacet;
        }

        private IMongoCollection<TEntity> GetCollection()
        {
            return db.GetCollection<TEntity>(_collectionName);
        }

        private FilterDefinition<TEntity> PrepareFilter(PagedRequest options)
        {
            var builder = Builders<TEntity>.Filter;
            var filter = builder.Empty;

            foreach (var item in options.SearchFields)
                filter = filter & builder.Eq(item.Key, item.Value);

            return filter;
        }
        private SortDefinition<TEntity> PrepareSorting(PagedRequest options)
        {
            var builder = Builders<TEntity>.Sort;

            if (options.OrderBy.IsEmpty())
                return builder.Descending(x => x.Id);

            List<SortDefinition<TEntity>> sort = new List<SortDefinition<TEntity>>();

            foreach (var key in options.OrderBy)
            {
                var descending = key.StartsWith('-');
                var itemKey = descending ? key.Remove(0, 1) : key;

                sort.Add(descending ? builder.Descending(itemKey) : builder.Ascending(itemKey));
            }

            return builder.Combine(sort);
        }
        #endregion Utilities
    }
}