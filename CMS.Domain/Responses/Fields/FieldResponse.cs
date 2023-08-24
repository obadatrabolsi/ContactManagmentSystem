using AutoMapper;
using CMS.Domain.Entities.ExtendedFields;
using CMS.Domain.Enums;

namespace CMS.Domain.Responses.Fields
{
    public class FieldResponse
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public FieldType FieldType { get; set; }
        public EntityName EntityName { get; set; }
    }

    public class FieldResponseProfile : Profile
    {
        public FieldResponseProfile()
        {
            CreateMap<ExtendedField, FieldResponse>();
        }
    }
}
