using CMS.Core.Base;
using CMS.Core.Cache;
using CMS.Core.Constants;
using CMS.Core.Extensions;
using CMS.Core.Mappers;
using CMS.Domain.Entities.ExtendedFields;
using CMS.Domain.Enums;
using CMS.Domain.Requests.Fields;
using CMS.Domain.Responses.Fields;
using CMS.Services.Base;
using Microsoft.AspNetCore.Http;

namespace CMS.Services.ExtendedFields
{
    public class ExtendedFieldService : MongoBaseService<ExtendedField>, IExtendedFieldService
    {
        #region Props & Ctor

        private readonly ICacheManager _cacheManager;
        public ExtendedFieldService(IMongoRepository<ExtendedField> repository,
            ICacheManager cacheManager) : base(repository)
        {
            _cacheManager=cacheManager;
        }

        #endregion

        #region Methods

        public async Task<List<FieldResponse>> GetForEntityAsync<TEntity>()
        {
            var entityName = typeof(TEntity).Name;
            var entityNameEnum = Enum.Parse<EntityName>(entityName);

            var entityFields = await _repository.ListAsync(x => x.EntityName == entityNameEnum);

            return entityFields.Select(x => x.MapTo<FieldResponse>()).ToList();
        }
        public async Task<List<FieldResponse>> GetForEntityCachedAsync<TEntity>()
        {
            var key = string.Format(CacheKeys.FIELDS_BY_ENTITY, typeof(TEntity).Name);

            return await _cacheManager.Get(key, GetForEntityAsync<TEntity>);
        }

        public async Task CreateAsync(CreateFieldRequest model)
        {
            var entityToInsert = model.MapTo<ExtendedField>();

            await ValidateFieldAsync(model);

            await _repository.InsertAsync(entityToInsert);

            ClearCache(entityToInsert.EntityName.ToString());
        }
        public async Task ValidateForEntity<TEntity>(Dictionary<string, object> fieldToValidate)
        {
            if (fieldToValidate.IsEmpty())
                return;

            var entityFields = await GetAllEntityFieldsNames<TEntity>();

            var unExistsFields = fieldToValidate.Where(ex => !entityFields.Contains(ex.Key));

            if (!unExistsFields.IsEmpty())
            {
                var invalidFields = string.Join(",", unExistsFields);
                throw new BadHttpRequestException(string.Format("The following fields in not valid to the given entity: {0}", invalidFields));
            }
        }

        #endregion

        #region Utilities

        private async Task ValidateFieldAsync(CreateFieldRequest model)
        {
            var isExists = await _repository.AnyAsync(x => x.FieldName.ToLower() == model.FieldName &&
                                                           x.EntityName == model.EntityName);

            if (isExists)
                throw new BadHttpRequestException("The entered field is already exists");
        }
        private async Task<List<string>> GetAllEntityFieldsNames<TEntity>()
        {
            var typeFields = typeof(TEntity).GetPropertiesList();
            var entityExtraFields = (await GetForEntityCachedAsync<TEntity>()).Select(x => x.FieldName).ToList();

            return typeFields.Concat(entityExtraFields).ToList();
        }

        private void ClearCache(string entityName)
        {
            var key = string.Format(CacheKeys.FIELDS_BY_ENTITY, entityName);

            _cacheManager.Remove(key);
        }

        #endregion
    }
}
