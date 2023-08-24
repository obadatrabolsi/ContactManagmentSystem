namespace CMS.Core.Extensions
{
    public static class ListExtensions
    {
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool IsEmpty<T>(this T[] list)
        {
            return list == null || list.Length == 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.Count() == 0;
        }
    }
}
