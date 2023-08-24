using AutoMapper;
using CMS.Core.Extensions;
using CMS.Domain.Entities.ExtendedFields;
using CMS.Domain.Enums;
using CMS.Domain.Requests.Fields;
using CMS.Domain.Responses.Fields;
using Microsoft.AspNetCore.Http;
using YIT.Core.Base;
using YIT.Services.Base;

namespace CMS.Services.ExtendedFields
{
    public class ExtendedFieldService : MongoBaseService<ExtendedField>, IExtendedFieldService
    {
        private readonly IMapper _mapper;

        public ExtendedFieldService(IMongoRepository<ExtendedField> repository,
            IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async Task<List<FieldResponse>> GetForEntityAsync<TEntity>()
        {
            var entityName = typeof(TEntity).Name;
            var entityNameEnum = Enum.Parse<EntityName>(entityName);

            var entityFields = await _repository.ListAsync(x => x.EntityName == entityNameEnum);

            return entityFields.Select(_mapper.Map<FieldResponse>).ToList();
        }
        public async Task<List<FieldResponse>> GetForEntityCachedAsync<TEntity>()
        {
            // Show enable caching
            var entityName = typeof(TEntity).Name;
            var entityNameEnum = Enum.Parse<EntityName>(entityName);

            var entityFields = await _repository.ListAsync(x => x.EntityName == entityNameEnum);

            return entityFields.Select(_mapper.Map<FieldResponse>).ToList();
        }

        public async Task CreateAsync(CreateFieldRequest model)
        {
            var entityToInsert = _mapper.Map<ExtendedField>(model);

            await ValidateFieldAsync(model);

            await _repository.InsertAsync(entityToInsert);
        }

        public async Task ValidateForEntity<TEntity>(List<EntityFieldRequest> fieldToValidate)
        {
            ValidateEntityImplicitFields<TEntity>(fieldToValidate);

            await ValidateEntityExtraFields<TEntity>(fieldToValidate);
        }

        private async Task ValidateFieldAsync(CreateFieldRequest model)
        {
            var isExists = await _repository.AnyAsync(x => x.FieldName.Trim().ToLower() == model.FieldName.Trim().ToLower() &&
                                                           x.EntityName == model.EntityName);

            if (isExists)
                throw new BadHttpRequestException("The entered field is already exists");
        }

        public void ValidateEntityImplicitFields<TEntity>(List<EntityFieldRequest> fieldToValidate)
        {
            var typeFields = typeof(TEntity).ToDictionary();

            var unExistsFields = fieldToValidate.Where(ex => !typeFields.Any(f => f.Key == ex.FieldName));

            if (!unExistsFields.IsEmpty())
            {
                var invalidFields = string.Join(",", unExistsFields.Select(x => x.FieldName));
                throw new BadHttpRequestException(string.Join("The following fields in not valid to the given entity: {0}", invalidFields));
            }
        }
        public async Task ValidateEntityExtraFields<TEntity>(List<EntityFieldRequest> fieldToValidate)
        {
            var entityExtraFields = await GetForEntityCachedAsync<TEntity>();

            var unExistsFields = fieldToValidate.Where(ex => !entityExtraFields.Exists(f => f.FieldName == ex.FieldName));

            if (!unExistsFields.IsEmpty())
            {
                var invalidFields = string.Join(",", unExistsFields.Select(x => x.FieldName));
                throw new BadHttpRequestException(string.Join("The following fields in not valid to the given entity: {0}", invalidFields));
            }
        }

    }
}
