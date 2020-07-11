using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using WayToEarth.GameLogic;
using static WayToEarth.Phisic.PhisicalObject;
using Action = WayToEarth.Phisic.PhisicalObject.Action;

namespace WayToEarth.Phisic
{
    interface PhisicalObject
    {
        MaterialPoint mp { get => new MaterialPoint(); set { } }
        RotatCharacter rc { get => new RotatCharacter(); set { } }

        Coord coord { get => mp.coord; set { mp.coord = value; } }
        Coord speed { get => mp.speed; set { mp.speed = value; } }
        double mass { get => mp.mass; set { mp.mass = value; } }

        double x { get => coord.x; set { coord.x = value; } }
        double y { get => coord.y; set { coord.y = value; } }
        double Vx { get => speed.x; set { speed.x = value; } }
        double Vy { get => speed.y; set { speed.y = value; } }

        double angle { get => rc.angle; set { rc.angle = value; } }
        double angulVel { get => rc.angulVel; set { rc.angulVel = value; } }
        double inertMoment { get => rc.inertMoment; set { rc.inertMoment = value; } }



        delegate void Interaction(PhisicalObject po1, PhisicalObject po2, double timeInSec);
        delegate void Action(PhisicalObject po, double timeInSec);
        delegate bool InteractCondition(PhisicalObject po1, PhisicalObject po2, double timeInSec);
        delegate bool ActCondition(PhisicalObject po, double timeInSec);



        [JsonIgnore]
        Interaction InteractionWithAll { get; set; }
        [JsonIgnore]
        Action ActionAlways { get; set; }

        [JsonIgnore]
        List<KeyValuePair<InteractCondition, Interaction>> InteractToCondit { get; set; }
        [JsonIgnore]
        List<KeyValuePair<ActCondition, Action>> ActToCondit { get; set; }



        string strInteractionWithAll
        {
            get => InteractionWithAll.NameInMap();
            set { InteractionWithAll = value.MethodInMap<PhisicalObject.Interaction>(); }
        }
        string strActionAlways
        {
            get => ActionAlways.NameInMap();
            set { ActionAlways = value.MethodInMap<PhisicalObject.Action>(); }
        }

        string strInteractToCondit
        {
            get => InteractToCondit.MethodsPairsToNamesP();
            set
            {
                InteractToCondit = value.NamesPairsToMethodsP<
                        PhisicalObject.InteractCondition,
                        PhisicalObject.Interaction
                    >();
            }
        }
        string strActToCondit
        {
            get => ActToCondit.MethodsPairsToNamesP();
            set
            {
                ActToCondit = value.NamesPairsToMethodsP<
                        PhisicalObject.ActCondition,
                        PhisicalObject.Action
                    >();
            }
        }



        PhisicalObject Move(double timeInSec)
        {
            mp.Move(timeInSec);

            rc.Move(timeInSec);

            return this;
        }

        PhisicalObject AddLinearAcceleration(Coord a, double timeInSec)
        {
            mp.AddLinearAcceleration(a, timeInSec);
            return this;
        }

        PhisicalObject ActForce(Coord F, double timeInSec)
        {
            mp.ActForce(F, timeInSec);
            return this;
        }

        PhisicalObject AddImpulse(Coord p)
        {
            mp.AddImpulse(p);
            return this;
        }

        PhisicalObject AddAngularAcceleration(double e, double timeInSec)
        {
            rc.AddAcceleration(e, timeInSec);
            return this;
        }

        PhisicalObject ActMomentOfForce(double M, double timeInSec)
        {
            rc.ActMomentOfForce(M, timeInSec);
            return this;
        }

        PhisicalObject AddMomentOfImpulse(double L)
        {
            rc.AddMomentOfImpulse(L);
            return this;
        }
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
