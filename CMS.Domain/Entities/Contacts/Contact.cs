using CMS.Core.Domain.Companies;
using MongoDB.Bson.Serialization.Attributes;
using YIT.Core.Base;

namespace CMS.Core.Domain.Contacts
{
    [BsonIgnoreExtraElements]
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public Company Company { get; set; }
    }
}
