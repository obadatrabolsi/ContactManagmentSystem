using CMS.Api.Controllers.Base;
using CMS.Core.Models;
using CMS.Domain.Requests.Companies;
using CMS.Domain.Responses.Companies;
using CMS.Services.Companies;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    public class CompanyController : BaseApiController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService=companyService;
        }

        [HttpPost("list")]
        public async Task<ApiResponse<PagedResponse<CompanyResponse>>> ListGridAsync(PagedRequest options)
        {
            var result = await _companyService.ListPaginatedAsync(options);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<CompanyResponse>> GetAsync(Guid id)
        {
            var company = await _companyService.GetAsync(id);

            return company;
        }

        [HttpPost("")]
        public async Task<ApiResponse> CreateAsync(CreateCompanyRequest model)
        {
            await _companyService.CreateAsync(model);

            return ApiResponse.Success();
        }

        [HttpPut("")]
        public async Task<ApiResponse> UpdateAsync(UpdateCompanyRequest model)
        {
            await _companyService.UpdateAsync(model);

            return ApiResponse.Success();
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteAsync(Guid id)
        {
            await _companyService.DeleteAsync(id);

            return ApiResponse.Success();
        }
    }
}
