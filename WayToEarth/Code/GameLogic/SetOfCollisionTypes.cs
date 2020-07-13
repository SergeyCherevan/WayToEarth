using System;
using System.Collections.Generic;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.GameLogic
{
    public class SetOfCollisionTypes
    {
        public Dictionary<KeyValuePair<Type, Type>, Interaction> map;

        public SetOfCollisionTypes()
        {
            map = new Dictionary<KeyValuePair<Type, Type>, Interaction>();
        }

        public Interaction this[Type t1, Type t2]
        {
            get => map[new KeyValuePair<Type, Type>(t1, t2)];
            set { map[new KeyValuePair<Type, Type>(t1, t2)] = value; }
        }
        public bool ContainsKey(Type t1, Type t2)
        {
            return map.ContainsKey(new KeyValuePair<Type, Type>(t1, t2));
        }

        public void Add(Type t1, Type t2, Interaction interact)
        {
            map.Add(new KeyValuePair<Type, Type>(t1, t2), interact);
        }
    }
}
