using CMS.Core.Base;
using CMS.Domain.Entities.Companies;
using MongoDB.Bson.Serialization.Attributes;

namespace CMS.Domain.Entities.Contacts
{
    [BsonIgnoreExtraElements]
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public List<Company> Companies { get; set; }
        public Dictionary<string, string> ExtendedFields { get; set; }
    }
}
