using CMS.Core.Base;
using CMS.Core.Cache;
using CMS.Core.Constants;
using CMS.Core.Mappers;
using CMS.Core.Models;
using CMS.Domain.Entities.Companies;
using CMS.Domain.Requests.Companies;
using CMS.Domain.Responses.Companies;
using CMS.Services.Base;
using CMS.Services.ExtendedFields;
using MongoDB.Driver;

namespace CMS.Services.Companies
{
    public class CompanyService : MongoBaseService<Company>, ICompanyService
    {
        #region Props & Ctor
        private readonly IExtendedFieldService _extendedFieldService;
        private readonly ICacheManager _cacheManager;

        public CompanyService(IMongoRepository<Company> repository,
            IExtendedFieldService extendedFieldService,
            ICacheManager cacheManager) : base(repository)
        {
            _extendedFieldService=extendedFieldService;
            _cacheManager=cacheManager;
        }

        #endregion

        #region Methods
        public async Task<PagedResponse<CompanyResponse>> ListPaginatedAsync(PagedRequest options)
        {
            var result = await _repository.ListPaginatedAsync(x => x.MapTo<CompanyResponse>(), options);

            return result;
        }
        public async Task<List<CompanyResponse>> GetAsync()
        {
            var companies = await _repository.ListAllAsync();

            return companies.Select(x => x.MapTo<CompanyResponse>()).ToList();
        }
        public async Task<CompanyResponse> GetAsync(Guid id)
        {
            var company = await _repository.FindAsync(id);

            if (company is null) return null;

            return company.MapTo<CompanyResponse>();
        }
        public async Task<List<Company>> GetAsync(List<Guid> ids)
        {
            return await _repository.ListAsync(x => ids.Contains(x.Id));
        }

        public async Task<Company> GetCachedAsync(Guid id)
        {
            var key = string.Format(CacheKeys.COMPANY_BY_ID, id);

            return await _cacheManager.Get(key, async () =>
            {
                return await _repository.FindAsync(id);
            });
        }


        public async Task CreateAsync(CreateCompanyRequest model)
        {
            var entityToInsert = model.MapTo<Company>();

            await _extendedFieldService.ValidateForEntity<Company>(model.ExtendedFields);

            await _repository.InsertAsync(entityToInsert);
        }
        public async Task UpdateAsync(UpdateCompanyRequest model)
        {
            var entityToUpdate = model.MapTo<Company>();

            await _extendedFieldService.ValidateForEntity<Company>(model.ExtendedFields);

            await _repository.UpdateAsync(entityToUpdate);

            ClearCache(model.Id);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

            ClearCache(id);
        }

        #endregion

        #region Utilities

        private void ClearCache(Guid id)
        {
            var key = string.Format(CacheKeys.COMPANY_BY_ID, id);

            _cacheManager.Remove(key);
        }
        #endregion
    }
}
