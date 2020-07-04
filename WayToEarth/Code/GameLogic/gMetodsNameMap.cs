using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WayToEarth.GameLogic;

namespace WayToEarth.GameLogic
{
    static class ActionNameMap
    {
        public static Dictionary<string, GameObject.Action> map;

        static ActionNameMap()
        {
            map = new Dictionary<string, GameObject.Action>();
        }

        public static GameObject.Action GetAction(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"ActionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.Action action) => map.KeyOf(action);
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

    static class InteractionNameMap
    {
        public static Dictionary<string, GameObject.Interaction> map;

        static InteractionNameMap()
        {
            map = new Dictionary<string, GameObject.Interaction>();
        }

        public static GameObject.Interaction GetInteraction(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"InteractionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.Interaction interaction) => map.KeyOf(interaction);
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
