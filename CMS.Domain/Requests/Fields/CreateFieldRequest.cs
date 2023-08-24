using AutoMapper;
using CMS.Core.Validations;
using CMS.Domain.Entities.ExtendedFields;
using CMS.Domain.Enums;
using FluentValidation;

namespace CMS.Domain.Requests.Fields
{
    public class CreateFieldRequest
    {
        public string FieldName { get; set; }
        public FieldType FieldType { get; set; }
        public EntityName EntityName { get; set; }
    }

    public class CreateFieldRequestValidator : AbstractValidator<CreateFieldRequest>
    {
        public CreateFieldRequestValidator()
        {
            RuleFor(x => x.FieldName).Required();
            RuleFor(x => x.FieldType).Required();
            RuleFor(x => x.EntityName).Required();
        }
    }

    public class CreateFieldRequestProfile : Profile
    {
        public CreateFieldRequestProfile()
        {
            CreateMap<CreateFieldRequest, ExtendedField>();
        }
    }
}
