using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WayToEarth.GameLogic;

namespace WayToEarth.GameLogic
{
    static class MethodNameMap<Method>
        where Method : MulticastDelegate
    {
        public static Dictionary<string, Method> map;

        static MethodNameMap()
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

        public static string GetName(Method action)
        {
            Delegate[] listOfDelegate = action.GetInvocationList();

            if (listOfDelegate.Length > 1)
            {
                List<string> listOfNames = new List<string>();

                foreach (var deleg in listOfDelegate)
                {
                    listOfNames.Add(GetName(deleg as Method));
                }

                return String.Join("\n", listOfNames);
            }

            string ret = map.KeyOf(action);

            if (ret == null)
                throw new ApplicationException(
                        $"MethodNameMap<{typeof(Method).Name}> has not got value \"{action.Method}\""
                    );

            return ret;
        }
    }
}