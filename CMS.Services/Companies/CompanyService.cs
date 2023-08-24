using AutoMapper;
using CMS.Core.Domain.Companies;
using CMS.Domain.Requests.Companies;
using CMS.Domain.Responses.Companies;
using CMS.Services.ExtendedFields;
using Quivyo.Core.Models;
using YIT.Core.Base;
using YIT.Services.Base;

namespace CMS.Services.Companies
{
    public class CompanyService : MongoBaseService<Company>
    {
        private readonly IMapper _mapper;
        private readonly IExtendedFieldService _extendedFieldService;

        public CompanyService(IMongoRepository<Company> repository, IMapper mapper,
            IExtendedFieldService extendedFieldService) : base(repository)
        {
            _mapper=mapper;
            _extendedFieldService=extendedFieldService;
        }

        public async Task<PagedResponse<CompanyResponse>> GetAsync(PagedRequest options)
        {
            var result = await _repository.ListPaginatedAsync(_mapper.Map<CompanyResponse>, options);

            return result;
        }
        public async Task<List<CompanyResponse>> GetAsync()
        {
            var companies = await _repository.ListAllAsync();

            return companies.Select(_mapper.Map<CompanyResponse>).ToList();
        }


        public async Task CreateAsync(CreateCompanyRequest model)
        {
            var entityToInsert = _mapper.Map<Company>(model);

            await _extendedFieldService.ValidateForEntity<Company>(model.ExtendedFields);

            await _repository.InsertAsync(entityToInsert);
        }
        public async Task UpdateAsync(UpdateCompanyRequest model)
        {
            var entityToUpdate = _mapper.Map<Company>(model);

            await _extendedFieldService.ValidateForEntity<Company>(model.ExtendedFields);

            await _repository.UpdateAsync(entityToUpdate);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
