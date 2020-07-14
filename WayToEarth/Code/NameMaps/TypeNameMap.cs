using System;
using System.Collections.Generic;
using WayToEarth.GameLogic;

namespace WayToEarth.NameMaps
{
    public class TypeNameMap
    {
        public static Dictionary<string, Type> map;

        public static void Start()
        {
            map = new Dictionary<string, Type>();

            map.Add("", null);
        }

        public static void AddType(Type type)
        {
            map.Add(type.Name, type);
        }

        public static Type GetType(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"TypeNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(Type type) => map.KeyOf(type);
    }

    public static class HelpStaticClassForTypeNameMap
    {
        public static Type TypeInMap(this string str)
        {
            return TypeNameMap.GetType(str);
        }

        public static string NameInMap(this Type type)
        {
            return TypeNameMap.GetName(type);
        }

        public static string TypesPairsToNamesP(this SetOfCollisionTypes typesMetods)
        {
            List<string> retList = new List<string>();

            foreach (var item in typesMetods.map)
            {
                retList.Add(item.Key.Key.NameInMap() + "\t" + item.Key.Value.NameInMap() + "\t" + item.Value.NameInMap());
            }

            return String.Join("\n", retList);
        }

        public static SetOfCollisionTypes NamesPairsToTypesP(this string str)
        {
            var typesMethods = new SetOfCollisionTypes();

            if (str == "") return typesMethods;

            var stringsPairs = str.Split("\n");

            foreach (var pairCondInteract in stringsPairs)
            {
                var trinity = pairCondInteract.Split("\t");

                typesMethods.Add(
                        trinity[0].TypeInMap(),
                        trinity[1].TypeInMap(),
                        trinity[2].MethodInMap<GameObject.Interaction>()
                    );
            }

            return typesMethods;
        }
    }
}
