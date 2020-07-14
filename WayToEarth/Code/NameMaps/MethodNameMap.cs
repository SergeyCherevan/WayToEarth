using System;
using System.Collections.Generic;

namespace WayToEarth.NameMaps
{
    public class MethodNameMap<Method>
        where Method : MulticastDelegate
    {
        public static Dictionary<string, Method> map;

        public static void Start()
        {
            map = new Dictionary<string, Method>();

            map.Add("", null);
        }

        public static void AddMethod(Method method)
        {
            map.Add(method.Method.ToString(), method);
        }

        public static Method GetMethod(string s)
        {
            string[] strList = s.Split("\n");

            if (strList.Length > 1)
            {
                Method ret = null;

                foreach (string str in strList)
                {
                    ret = MulticastDelegate.Combine(ret, GetMethod(str)) as Method;
                }

                return ret;
            }

            if (!map.ContainsKey(s))
                throw new ApplicationException($"MethodNameMap<{typeof(Method).Name}> has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(Method method)
        {
            if (method == null) return "";

            Delegate[] listOfDelegate = method.GetInvocationList();

            if (listOfDelegate.Length > 1)
            {
                List<string> listOfNames = new List<string>();

                foreach (var deleg in listOfDelegate)
                {
                    listOfNames.Add(GetName(deleg as Method));
                }

                return String.Join("\n", listOfNames);
            }

            string ret = map.KeyOf(method);

            if (ret == null)
                throw new ApplicationException(
                        $"MethodNameMap<{typeof(Method).Name}> has not got value \"{method.Method}\""
                    );

            return ret;
        }
    }

    static public class HelpStaticClassForMethodNameMap
    {
        public static Method MethodInMap<Method>(this string str)
            where Method : MulticastDelegate
        {
            return MethodNameMap<Method>.GetMethod(str);
        }

        public static string NameInMap<Method>(this Method method)
            where Method : MulticastDelegate
        {
            return MethodNameMap<Method>.GetName(method);
        }

        public static string
                MethodsPairsToNamesP<Method1, Method2>(this List<KeyValuePair<Method1, Method2>> methodsPairs)
                    where Method1 : MulticastDelegate
                    where Method2 : MulticastDelegate
        {
            List<string> retList = new List<string>();

            foreach (var pairCondInteract in methodsPairs)
            {
                retList.Add(pairCondInteract.Key.NameInMap() + "\t" + pairCondInteract.Value.NameInMap());
            }

            return String.Join("\n", retList);
        }

        public static
            List<KeyValuePair<Method1, Method2>>
                NamesPairsToMethodsP<Method1, Method2>(this string str)
                    where Method1 : MulticastDelegate
                    where Method2 : MulticastDelegate
        {
            var retList = new List<KeyValuePair<Method1, Method2>>();

            if (str == "") return retList;

            var stringsPairs = str.Split("\n");

            foreach (var pairCondInteract in stringsPairs)
            {
                var pair = pairCondInteract.Split("\t");

                retList.Add(
                        new KeyValuePair<Method1, Method2>(
                                pair[0].MethodInMap<Method1>(),
                                pair[1].MethodInMap<Method2>()
                            )
                    );
            }

            return retList;
        }
    }
}