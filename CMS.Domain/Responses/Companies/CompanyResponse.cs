using AutoMapper;
using CMS.Domain.Entities.Companies;

namespace CMS.Domain.Responses.Companies
{
    public class CompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public Dictionary<string, object> ExtendedFields { get; set; }
    }

    public class CompanyResponseProfile : Profile
    {
        public CompanyResponseProfile()
        {
            CreateMap<Company, CompanyResponse>();
        }
    }
}
