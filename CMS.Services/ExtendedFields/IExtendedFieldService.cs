using CMS.Domain.Requests.Fields;

namespace CMS.Services.ExtendedFields
{
    public interface IExtendedFieldService
    {
        Task CreateAsync(CreateFieldRequest model);
        Task ValidateForEntity<TEntity>(Dictionary<string, object> fieldToValidate);
    }
}