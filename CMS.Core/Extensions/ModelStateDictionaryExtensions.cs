using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Quivyo.Core.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static Dictionary<string, string> GetErrorsDictionary(this ModelStateDictionary target)
        {
            Dictionary<string, string> errorsList = new Dictionary<string, string>();

            foreach (KeyValuePair<string, ModelStateEntry> keyModelStatePair in target)
            {
                string key = keyModelStatePair.Key;
                ModelErrorCollection errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    foreach (var error in errors)
                    {
                        var errorMessage = error.ErrorMessage ?? error.Exception?.Message ?? "";

                        if (!errorsList.ContainsKey(key))
                            errorsList.Add(key, errorMessage);
                        else
                            errorsList[key] = $"{errorsList[key]}{Environment.NewLine}{errorMessage}";
                    }
                }
            }

            return errorsList;
        }
    }
}
