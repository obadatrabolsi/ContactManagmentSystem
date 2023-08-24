using CMS.Core.Domain.Contacts;
using YIT.Core.Base;
using YIT.Services.Base;

namespace CMS.Services.Contacts
{
    public class ContactService : MongoBaseService<Contact>
    {
        public ContactService(IMongoRepository<Contact> repository) : base(repository)
        {
        }
    }
}
