using CMS.Core.Models;
using CMS.Domain.Requests.Contacts;
using CMS.Domain.Responses.Contacts;

namespace CMS.Services.Contacts
{
    public interface IContactService
    {
        Task CreateAsync(CreateContactRequest model);
        Task DeleteAsync(Guid id);
        Task<List<ContactResponse>> GetAsync();
        Task<ContactResponse> GetAsync(Guid id);
        Task<PagedResponse<ContactResponse>> ListPaginatedAsync(PagedRequest options);
        Task UpdateAsync(UpdateContactRequest model);
    }
}