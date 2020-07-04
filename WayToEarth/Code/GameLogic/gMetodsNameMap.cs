using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WayToEarth.GameLogic;

namespace WayToEarth.GameLogic
{
    static class gActionNameMap
    {
        public static Dictionary<string, GameObject.gAction> map;

        static gActionNameMap()
        {
            map = new Dictionary<string, GameObject.gAction>();
        }

        public static GameObject.gAction GetAction(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"gActionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.gAction action) => map.KeyOf(action);
    }

    static class gConditActionNameMap
    {
        public static Dictionary<string, GameObject.ActCondition> map;

        static gConditActionNameMap()
        {
            map = new Dictionary<string, GameObject.ActCondition>();
        }

        public static GameObject.ActCondition GetAction(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"gConditActionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.ActCondition condit) => map.KeyOf(condit);
    }

    static class gInteractionNameMap
    {
        public static Dictionary<string, GameObject.gInteraction> map;

        static gInteractionNameMap()
        {
            map = new Dictionary<string, GameObject.gInteraction>();
        }

        public static GameObject.gInteraction GetInteraction(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"gInteractionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.gInteraction interaction) => map.KeyOf(interaction);
    }

    static class gConditInteractionNameMap
    {
        public static Dictionary<string, GameObject.InteractCondition> map;

        static gConditInteractionNameMap()
        {
            map = new Dictionary<string, GameObject.InteractCondition>();
        }

        public static GameObject.InteractCondition GetAction(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"gConditInteractionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.InteractCondition condit) => map.KeyOf(condit);
    }
}
