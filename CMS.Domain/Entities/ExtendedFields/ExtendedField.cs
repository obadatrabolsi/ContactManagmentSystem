using CMS.Core.Base;
using CMS.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace CMS.Domain.Entities.ExtendedFields
{
    [BsonIgnoreExtraElements]
    public class ExtendedField : BaseEntity
    {
        public string FieldName { get; set; }
        public FieldType FieldType { get; set; }
        public EntityName EntityName { get; set; }
    }
}
