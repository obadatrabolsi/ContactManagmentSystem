using AutoMapper;
using CMS.Core.Domain.Companies;
using CMS.Domain.Requests.Fields;

namespace CMS.Domain.Responses.Companies
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public List<EntityFieldRequest> ExtendedFields { get; set; }
    }

    public class CompanyResponseProfile : Profile
    {
        public CompanyResponseProfile()
        {
            CreateMap<Company, CompanyResponse>();
        }
    }
}
