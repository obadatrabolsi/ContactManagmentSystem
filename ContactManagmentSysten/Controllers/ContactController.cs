using CMS.Api.Controllers.Base;
using CMS.Core.Models;
using CMS.Domain.Requests.Contacts;
using CMS.Domain.Responses.Contacts;
using CMS.Services.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Api.Controllers
{
    public class ContactController : BaseApiController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService=contactService;
        }

        [HttpPost("list")]
        public async Task<ApiResponse<PagedResponse<ContactResponse>>> ListGridAsync(PagedRequest options)
        {
            var result = await _contactService.ListPaginatedAsync(options);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<ContactResponse>> GetAsync(Guid id)
        {
            var contact = await _contactService.GetAsync(id);

            return contact;
        }

        [HttpPost("")]
        public async Task<ApiResponse> CreateAsync(CreateContactRequest model)
        {
            await _contactService.CreateAsync(model);

            return ApiResponse.Success();
        }

        [HttpPut("")]
        public async Task<ApiResponse> UpdateAsync(UpdateContactRequest model)
        {
            await _contactService.UpdateAsync(model);

            return ApiResponse.Success();
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteAsync(Guid id)
        {
            await _contactService.DeleteAsync(id);

            return ApiResponse.Success();
        }
    }
}
