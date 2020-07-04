using System;
using System.Collections.Generic;
using System.Text;

namespace WayToEarth.Addition
{
    public class DictionaryInd<T1, T2, T3>
    {
        public Dictionary<KeyValuePair<T1, T2>, T3> dictionary;

        public DictionaryInd(Dictionary<KeyValuePair<T1, T2>, T3> dic)
        {
            dictionary = dic;
        }

        public T3 this[T1 i1, T2 i2]
        {
            get => dictionary[new KeyValuePair<T1, T2>(i1, i2)];
            set { dictionary[new KeyValuePair<T1, T2>(i1, i2)] = value; }
        }
    }

    static class Addition
    {
        public static bool ContainsKey<T1, T2, T3>(this Dictionary<KeyValuePair<T1, T2>, T3> dic, T1 k1, T2 k2)
        {
            return dic.ContainsKey(new KeyValuePair<T1, T2>(k1, k2));
        }

        public static void Add<T1, T2, T3>(this Dictionary<KeyValuePair<T1, T2>, T3> dic, T1 k1, T2 k2, T3 v)
        {
            dic.Add(new KeyValuePair<T1, T2>(k1, k2), v);
        }

        public static DictionaryInd<T1, T2, T3> o<T1, T2, T3>(this Dictionary<KeyValuePair<T1, T2>, T3> dic)
        {
            return new DictionaryInd<T1, T2, T3>(dic);
        }
    }
}
