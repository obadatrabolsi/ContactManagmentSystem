namespace CMS.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> me, Dictionary<TKey, TValue> merge)
        {
            foreach (var item in merge)
            {
                if (!me.ContainsKey(item.Key))
                    me.Add(item.Key, item.Value);
            }

            return me;
        }
    }
}
