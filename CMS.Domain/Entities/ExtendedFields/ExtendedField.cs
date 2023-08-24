using CMS.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using YIT.Core.Base;

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
