using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI
{
    public static class DictionaryExtensions
    {
        public static void RemoveAll<TKey, TValue>(this Dictionary<TKey, TValue> dic, Func<TValue, bool> predicate)
        {
            var keys = dic.Keys.Where(k => predicate(dic[k])).ToList();
            foreach (var key in keys)
            {
                dic.Remove(key);
            }
        }

        public static List<TValue> RemoveAllKeys<TKey, TValue>(this Dictionary<TKey, TValue> dic, Func<TKey, bool> predicate)
        {
            var keys = dic.Keys.Where(k => predicate(k)).ToList();
            var values = dic.Where(v => keys.Contains(v.Key)).Select(v => dic[v.Key]).ToList();

            foreach (var key in keys)
            {
                dic.Remove(key);
            }

            return values;
        }
    }
}
