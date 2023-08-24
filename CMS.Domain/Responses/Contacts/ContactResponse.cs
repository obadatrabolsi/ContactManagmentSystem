using AutoMapper;
using CMS.Domain.Entities.Contacts;
using CMS.Domain.Responses.Companies;

namespace CMS.Domain.Responses.Contacts
{
    public class ContactResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CompanyResponse> Companies { get; set; }
        public Dictionary<string, object> ExtendedFields { get; set; }
    }

    public class ContactResponseProfile : Profile
    {
        public ContactResponseProfile()
        {
            CreateMap<Contact, ContactResponse>();
        }
    }
}
