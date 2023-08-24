using AutoMapper;
using CMS.Core.Validations;
using CMS.Domain.Entities.Companies;
using CMS.Domain.Entities.Contacts;
using FluentValidation;

namespace CMS.Domain.Requests.Contacts
{
    public class CreateContactRequest
    {
        public string Name { get; set; }
        public List<Guid> CompaniesIds { get; set; }
        public Dictionary<string, object> ExtendedFields { get; set; }
    }

    public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactRequestValidator()
        {
            RuleFor(x => x.Name).Required();
            RuleFor(x => x.CompaniesIds).Required();
        }
    }

    public class CreateContactRequestProfile : Profile
    {
        public CreateContactRequestProfile()
        {
            CreateMap<CreateContactRequest, Contact>()
                .ForMember(x => x.Companies, x => x.MapFrom(c => new List<Company>()));

            CreateMap<UpdateContactRequest, Contact>()
                .ForMember(x => x.Companies, x => x.MapFrom(c => new List<Company>())); ;
        }
    }
}
