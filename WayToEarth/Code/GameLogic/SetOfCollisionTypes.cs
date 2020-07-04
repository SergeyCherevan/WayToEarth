using System;
using System.Collections.Generic;
using System.Text;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.GameLogic
{
    public class SetOfCollisionTypes
    {
        public Dictionary<KeyValuePair<Type, Type>, gInteraction> dictionary;

        public SetOfCollisionTypes()
        {
            dictionary = new Dictionary<KeyValuePair<Type, Type>, gInteraction>();
        }

        public gInteraction this[Type t1, Type t2]
        {
            get => dictionary[new KeyValuePair<Type, Type>(t1, t2)];
            set { dictionary[new KeyValuePair<Type, Type>(t1, t2)] = value; }
        }
        public bool ContainsKey(Type t1, Type t2)
        {
            return dictionary.ContainsKey(new KeyValuePair<Type, Type>(t1, t2));
        }

        public void Add(Type t1, Type t2, gInteraction interact)
        {
            dictionary.Add(new KeyValuePair<Type, Type>(t1, t2), interact);
        }
    }
}
