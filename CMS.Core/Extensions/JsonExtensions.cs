using System.Text.Json;

namespace CMS.Core.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T jsonObject) where T : class
        {
            try
            {
                return jsonObject is null ? null : JsonSerializer.Serialize(jsonObject);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T ToObject<T>(this string jsonData) where T : class
        {
            try
            {
                return jsonData.IsNullOrEmpty() ? null : JsonSerializer.Deserialize<T>(jsonData);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
