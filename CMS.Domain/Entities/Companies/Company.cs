using CMS.Core.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace CMS.Domain.Entities.Companies
{
    [BsonIgnoreExtraElements]
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public int NumberOfEmployees { get; set; }
        public Dictionary<string, string> ExtendedFields { get; set; }
    }
}
