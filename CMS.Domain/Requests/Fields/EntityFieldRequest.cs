using AutoMapper;
using CMS.Core.Validations;
using CMS.Domain.Entities.ExtendedFields;
using CMS.Domain.Enums;
using FluentValidation;

namespace CMS.Domain.Requests.Fields
{
    public class EntityFieldRequest
    {
        public string FieldName { get; set; }
        public FieldType FieldType { get; set; }
        public object Value { get; set; }
    }

    public class EntityFieldRequestValidator : AbstractValidator<EntityFieldRequest>
    {
        public EntityFieldRequestValidator()
        {
            RuleFor(x => x.FieldName).Required();
            RuleFor(x => x.FieldType).Required();
            RuleFor(x => x.Value).Required();
        }
    }

    public class EntityFieldRequestProfile : Profile
    {
        public EntityFieldRequestProfile()
        {
            CreateMap<EntityFieldRequest, ExtendedField>().ReverseMap();
            CreateMap<EntityFieldRequest, KeyValuePair<string, object>>()
                .ForMember(x => x.Key, x => x.MapFrom(c => c.FieldName))
                .ForMember(x => x.Value, x => x.MapFrom(c => c.Value))
                ;
        }
    }
}
