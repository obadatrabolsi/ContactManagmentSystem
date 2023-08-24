using CMS.Core.Models;
using CMS.Domain.Entities.Companies;
using CMS.Domain.Requests.Companies;
using CMS.Domain.Responses.Companies;

namespace CMS.Services.Companies
{
    public interface ICompanyService
    {
        Task<PagedResponse<CompanyResponse>> ListPaginatedAsync(PagedRequest options);
        Task<List<CompanyResponse>> GetAsync();
        Task<CompanyResponse> GetAsync(Guid id);
        Task<List<Company>> GetAsync(List<Guid> ids);
        Task<Company> GetCachedAsync(Guid id);
        Task CreateAsync(CreateCompanyRequest model);
        Task UpdateAsync(UpdateCompanyRequest model);
        Task DeleteAsync(Guid id);
    }
}