using CMS.Domain.Requests.Fields;

namespace CMS.Services.ExtendedFields
{
    public interface IExtendedFieldService
    {
        Task CreateAsync(CreateFieldRequest model);
        Task<List<string>> GetAllEntityFieldsNames<TEntity>();
        Task<List<(string FieldName, bool IsExtendedField)>> GetAllEntityFields<TEntity>();
        Task ValidateForEntity<TEntity>(Dictionary<string, object> fieldToValidate);
    }
}