using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using static WayToEarth.Phisic.PhisicalObject;
using Action = WayToEarth.Phisic.PhisicalObject.Action;

namespace WayToEarth.Phisic
{
    public interface PhisicalObject
    {
        [JsonIgnore] public Complex coord { get; set; }
        public Complex speed { get; set; }
        public double mass { get; set; }

        public double angle { get; set; }
        public double angulVel { get; set; }
        public double inertMoment { get; set; }

        [JsonIgnore] public double x { get; set; }
        [JsonIgnore] public double y { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }



        public delegate void Interaction(PhisicalObject po1, PhisicalObject po2, double timeInSec);
        public delegate void Action(PhisicalObject po, double timeInSec);
        public delegate bool InteractCondition(PhisicalObject po1, PhisicalObject po2, double timeInSec);
        public delegate bool ActCondition(PhisicalObject po, double timeInSec);

        [JsonIgnore]
        public Interaction InteractionWithAll { get; set; }
        [JsonIgnore]
        public Action ActionAlways { get; set; }

        [JsonIgnore]
        public List<KeyValuePair<InteractCondition, Interaction>> InteractToCondit { get; set; }
        [JsonIgnore]
        public List<KeyValuePair<ActCondition, Action>> ActToCondit { get; set; }



        public PhisicalObject Move(double timeInSec);

        public PhisicalObject AddLinearAcceleration(Complex a, double timeInSec);

        public PhisicalObject ActForce(Complex F, double timeInSec);

        public PhisicalObject AddImpulse(Complex p);

        public PhisicalObject AddAngularAcceleration(double e, double timeInSec);

        public PhisicalObject ActMomentOfForce(double M, double timeInSec);

        public PhisicalObject AddMomentOfImpulse(double L);
    }



    class NullPhDelegate
    {
        public static void SetPhDelegateValue(PhisicalObject o)
        {
            o.InteractionWithAll = null;
            o.ActionAlways = null;

            o.InteractToCondit = new List<KeyValuePair<InteractCondition, Interaction>>();
            o.ActToCondit = new List<KeyValuePair<ActCondition, Action>>();
        }
    }
}
