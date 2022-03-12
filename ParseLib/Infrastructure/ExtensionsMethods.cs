using System.Collections;
using AngleSharp.Common;

namespace ParseLib.Infrastructure
{
    public static class ExtensionsMethods
    {
        public static bool IsThisLast<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey current)
        {
            TKey reference = dictionary.GetItemByIndex(dictionary.Count - 1).Key;
            return reference.Equals(current);
        }

        public static bool IsThisLast<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
            KeyValuePair<TKey, TValue> current)
        {
            TKey reference = dictionary.GetItemByIndex(dictionary.Count - 1).Key;
            return reference.Equals(current.Key);
        }

        public static void For<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var o in enumerable)
            {
                action.Invoke(o);
            }
        }
    }
}