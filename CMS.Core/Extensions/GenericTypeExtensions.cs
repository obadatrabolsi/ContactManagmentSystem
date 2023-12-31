﻿using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CMS.Core.Extensions
{
    public static class GenericTypeExtensions
    {
        public static Dictionary<string, object> ToDictionary<T>(this T c) where T : class
        {
            return typeof(T).ToDictionary();
        }

        public static Dictionary<string, object> ToDictionary(this Type type)
        {
            var classProperties = type
                    .GetProperties()
                    .Where(x => x.GetCustomAttribute<NotMappedAttribute>() == null &&
                                x.GetCustomAttribute<BsonIgnoreAttribute>() == null);

            return classProperties.ToDictionary(x => x.Name, x => (object)"");
        }

        public static List<string> GetPropertiesList(this Type type)
        {
            var classProperties = type
                    .GetProperties()
                    .Where(x => x.GetCustomAttribute<NotMappedAttribute>() == null &&
                                x.GetCustomAttribute<BsonIgnoreAttribute>() == null);

            return classProperties.Select(x => x.Name).ToList();
        }
    }
}
