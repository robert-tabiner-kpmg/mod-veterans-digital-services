using System.Collections.Generic;

namespace Forms.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> dictionaryToAdd)
        {
            foreach (var (key, value) in dictionaryToAdd)
            {
                dictionary.Add(key, value);
            }
        }
    }
}