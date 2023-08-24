using CMS.Domain.Entities.ExtendedFields;
using MongoDB.Bson.Serialization.Attributes;
using YIT.Core.Base;

namespace CMS.Core.Domain.Companies
{
    [BsonIgnoreExtraElements]
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public List<ExtendedField> ExtendedFields { get; set; }
    }
}
