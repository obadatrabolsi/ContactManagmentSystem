using AutoMapper;
using CMS.Core.Validations;
using CMS.Domain.Entities.Companies;
using FluentValidation;

namespace CMS.Domain.Requests.Companies
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public Dictionary<string, object> ExtendedFields { get; set; }
    }

    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidator()
        {
            RuleFor(x => x.Name).Required();
            RuleFor(x => x.NumberOfEmployees).Required().GreaterThan(0);
        }
    }

    public class CreateCompanyRequestProfile : Profile
    {
        public CreateCompanyRequestProfile()
        {
            CreateMap<CreateCompanyRequest, Company>();
            CreateMap<UpdateCompanyRequest, Company>();
        }
    }
}
