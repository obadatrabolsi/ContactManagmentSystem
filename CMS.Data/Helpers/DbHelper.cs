using CMS.Core.Settings;
using CMS.Domain.Entities.Companies;
using CMS.Domain.Entities.Contacts;
using MongoDB.Driver;

namespace CMS.Data.Helpers
{
    public class DbHelper
    {
        private readonly MongoDbSettings _mongoDbSettings;

        public DbHelper(MongoDbSettings mongoDbSettings)
        {
            _mongoDbSettings =  mongoDbSettings;
        }

        public async Task CreateIndexes()
        {
            await CreateIndexAsync<Company>(nameof(Company.Name));
            await CreateIndexAsync<Contact>(nameof(Contact.Name));
        }

        public async Task CreateIndexAsync<TCollection>(string fieldName)
        {

            try
            {
                var client = new MongoClient();
                var database = client.GetDatabase(_mongoDbSettings.Name);
                var collection = database.GetCollection<TCollection>(typeof(TCollection).Name);

                var options = new CreateIndexOptions() { Unique = true };
                var field = new StringFieldDefinition<TCollection>(fieldName);
                var indexDefinition = new IndexKeysDefinitionBuilder<TCollection>().Ascending(field);

                var indexModel = new CreateIndexModel<TCollection>(indexDefinition, options);
                await collection.Indexes.CreateOneAsync(indexModel);
            }
            catch
            {
            }
        }
    }
}
