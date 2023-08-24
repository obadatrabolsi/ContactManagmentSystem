using CMS.Core.Base;
using CMS.Core.Mappers;
using CMS.Core.Models;
using CMS.Domain.Entities.Contacts;
using CMS.Domain.Requests.Contacts;
using CMS.Domain.Responses.Contacts;
using CMS.Services.Base;
using CMS.Services.Companies;
using CMS.Services.ExtendedFields;
using Microsoft.AspNetCore.Http;

namespace CMS.Services.Contacts
{
    public class ContactService : MongoBaseService<Contact>, IContactService
    {
        #region Props & Ctor

        private readonly IExtendedFieldService _extendedFieldService;
        private readonly ICompanyService _companyService;

        public ContactService(IMongoRepository<Contact> repository,
            IExtendedFieldService extendedFieldService,
            ICompanyService companyService) : base(repository)
        {
            _extendedFieldService=extendedFieldService;
            _companyService=companyService;
        }

        #endregion

        #region Methods
        public async Task<PagedResponse<ContactResponse>> ListPaginatedAsync(PagedRequest options)
        {
            var result = await _repository.ListPaginatedAsync(x => x.MapTo<ContactResponse>(), options);

            return result;
        }
        public async Task<List<ContactResponse>> GetAsync()
        {
            var contacts = await _repository.ListAllAsync();

            return contacts.Select(x => x.MapTo<ContactResponse>()).ToList();
        }
        public async Task<ContactResponse> GetAsync(Guid id)
        {
            var contact = await _repository.FindAsync(id);

            if (contact is null) return null;

            return contact.MapTo<ContactResponse>();
        }

        public async Task CreateAsync(CreateContactRequest model)
        {
            var entityToInsert = model.MapTo<Contact>();

            await _extendedFieldService.ValidateForEntity<Contact>(model.ExtendedFields);

            await MapCompaniesAsync(model, entityToInsert);

            await _repository.InsertAsync(entityToInsert);
        }
        public async Task UpdateAsync(UpdateContactRequest model)
        {
            var isExists = await _repository.AnyAsync(x => x.Id == model.Id);

            if (!isExists)
                throw new Exception($"The requested contact is not found");

            var entityToUpdate = model.MapTo<Contact>();

            await _extendedFieldService.ValidateForEntity<Contact>(model.ExtendedFields);

            await MapCompaniesAsync(model, entityToUpdate);

            await _repository.UpdateAsync(entityToUpdate);
        }
        public async Task DeleteAsync(Guid id)
        {
            var isExists = await _repository.AnyAsync(x => x.Id == id);

            if (!isExists)
                throw new Exception($"The requested contact is not found");

            await _repository.DeleteAsync(id);
        }

        #endregion

        #region Utilities

        private async Task MapCompaniesAsync(CreateContactRequest model, Contact entityToInsert)
        {
            foreach (var companyId in model.CompaniesIds)
            {
                var company = await _companyService.GetCachedAsync(companyId);

                if (company is null)
                    throw new BadHttpRequestException($"The company id '{companyId}' is invalid");

                entityToInsert.Companies.Add(company);
            }
        }
        #endregion
    }
}
