using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CMS.Core.Extensions
{
    public static class StringExtensions
    {

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static int? ToInteger(this string str)
        {
            try
            {
                return Convert.ToInt32(str.Trim());
            }
            catch
            {
                return null;
            }
        }

        public static decimal? ToDecimal(this string str)
        {
            try
            {
                return Convert.ToDecimal(str.Trim());
            }
            catch
            {
                return null;
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }

        public static string ToCamelCase(this string text)
        {
            try
            {
                if (text.Length == 0) return "";

                text = Regex.Replace(text, "([A-Z])([A-Z]+)($|[A-Z])",
                    m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);

                return char.ToLower(text[0]) + text.Substring(1);
            }
            catch
            {
                return "";
            }
        }

        public static string GetEmailDomain(this string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                return address.Host;

            }
            catch
            {
                return "";
            }
        }

        public static int? GetIntegerValue(this string text)
        {
            try
            {
                return int.Parse(text);
            }
            catch
            {
                return null;
            }
        }
        public static long? GetInteger64Value(this string text)
        {
            try
            {
                return long.Parse(text);
            }
            catch
            {
                return null;
            }
        }
        public static bool? GetBooleanValue(this string text)
        {
            try
            {
                return bool.Parse(text);
            }
            catch
            {
                return null;
            }
        }
        public static List<long> GetLongYITay(this string text)
        {
            try
            {
                if (text.IsNullOrEmpty())
                    return new List<long>();

                var numbers = text.Split(",");

                return Array.ConvertAll(numbers, t => Convert.ToInt64(t)).ToList();
            }
            catch
            {
                return new List<long>();
            }
        }
    }
}
