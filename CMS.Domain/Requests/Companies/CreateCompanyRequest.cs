using AutoMapper;
using CMS.Core.Validations;
using CMS.Domain.Entities.ExtendedFields;
using CMS.Domain.Requests.Fields;
using FluentValidation;

namespace CMS.Domain.Requests.Companies
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public List<EntityFieldRequest> ExtendedFields { get; set; }
    }

    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidator()
        {
            RuleFor(x => x.Name).Required();
            RuleFor(x => x.NumberOfEmployees).Required();
            RuleForEach(x => x.ExtendedFields).SetValidator(new EntityFieldRequestValidator());
        }
    }

    public class CreateCompanyRequestProfile : Profile
    {
        public CreateCompanyRequestProfile()
        {
            CreateMap<CreateCompanyRequest, ExtendedField>();
        }
    }
}
