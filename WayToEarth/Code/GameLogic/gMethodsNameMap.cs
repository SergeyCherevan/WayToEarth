using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WayToEarth.GameLogic;

namespace WayToEarth.GameLogic
{
    static class gActionNameMap
    {
        public static Dictionary<string, GameObject.Action> map;

        static gActionNameMap()
        {
            map = new Dictionary<string, GameObject.Action>();
        }

        public static void AddMethod(GameObject.Action action)
        {
            map.Add(action.Method.ToString(), action);
        }

        public static GameObject.Action GetMethod(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"gActionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.Action action)
        {
            Delegate[] listOfDelegate = action.GetInvocationList();

            if (listOfDelegate.Length > 1)
            {
                List<string> listOfNames = new List<string>();

                foreach (var deleg in listOfDelegate)
                {
                    listOfNames.Add(GetName(deleg as GameObject.Action));
                }

                return String.Join("\n", listOfNames);
            }

            var ret = map.KeyOf(action);

            if (ret == null)
                throw new ApplicationException(
                        $"gActionNameMap has not got value \"{action.Method}\""
                    );

            return ret;
        }
    }

    static class gConditActionNameMap
    {
        public static Dictionary<string, GameObject.ActCondition> map;

        static gConditActionNameMap()
        {
            map = new Dictionary<string, GameObject.ActCondition>();
        }

        public static void AddMethod(GameObject.ActCondition action)
        {
            map.Add(action.Method.ToString(), action);
        }

        public static GameObject.ActCondition GetMethod(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"gConditActionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.ActCondition condit)
        {
            var ret = map.KeyOf(condit);

            if (ret == null)
                throw new ApplicationException(
                        $"gConditActionNameMap has not got value \"{condit.Method}\""
                    );

            return ret;
        }
    }

    static class gInteractionNameMap
    {
        public static Dictionary<string, GameObject.Interaction> map;

        static gInteractionNameMap()
        {
            map = new Dictionary<string, GameObject.Interaction>();
        }

        public static void AddMethod(GameObject.Interaction action)
        {
            map.Add(action.Method.ToString(), action);
        }

        public static GameObject.Interaction GetMethod(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"gInteractionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.Interaction interaction)
        {
            var ret = map.KeyOf(interaction);

            if (ret == null)
                throw new ApplicationException(
                        $"gInteractionNameMap has not got value \"{interaction.Method}\""
                    );

            return ret;
        }
    }

    static class gConditInteractionNameMap
    {
        public static Dictionary<string, GameObject.InteractCondition> map;

        static gConditInteractionNameMap()
        {
            map = new Dictionary<string, GameObject.InteractCondition>();
        }

        public static void AddMethod(GameObject.InteractCondition action)
        {
            map.Add(action.Method.ToString(), action);
        }

        public static GameObject.InteractCondition GetMethod(string s)
        {
            if (!map.ContainsKey(s))
                throw new ApplicationException($"gConditInteractionNameMap has not got key \"{s}\"");

            return map[s];
        }

        public static string GetName(GameObject.InteractCondition condit)
        {
            var ret = map.KeyOf(condit);

            if (ret == null)
                throw new ApplicationException(
                        $"gConditInteractionNameMap has not got value \"{condit.Method}\""
                    );

            return ret;
        }
    }
}
