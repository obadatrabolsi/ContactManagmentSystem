using YIT.Core.Base;

namespace YIT.Services.Base
{
    public class MongoBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IMongoRepository<TEntity> _repository;

        public MongoBaseService(IMongoRepository<TEntity> repository)
        {
            _repository = repository;
        }
    }
}