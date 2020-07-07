using Newtonsoft.Json;
using System.Collections.Generic;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using static WayToEarth.Phisic.PhisicalObject;
using Action = WayToEarth.Phisic.PhisicalObject.Action;

namespace WayToEarth
{
    class RigidBody : PhisicalObject
    {
        public MaterialPoint mp { get; set; }
        public RotatCharacter rc { get; set; }

        public Coord coord { get => mp.coord; set { mp.coord = value; } }
        public Coord speed { get => mp.speed; set { mp.speed = value; } }
        public double mass { get => mp.mass; set { mp.mass = value; } }

        double x { get => coord.x; set { coord.x = value; } }
        double y { get => coord.y; set { coord.y = value; } }
        double Vx { get => speed.x; set { speed.x = value; } }
        double Vy { get => speed.y; set { speed.y = value; } }

        public double angle { get => rc.angle; set { rc.angle = value; } }
        public double angulVel { get => rc.angulVel; set { rc.angulVel = value; } }
        public double inertMoment { get => rc.inertMoment; set { rc.inertMoment = value; } }



        [JsonIgnore]
        public Interaction InteractionWithAll { get; set; }
        [JsonIgnore]
        public Action ActionAlways { get; set; }

        [JsonIgnore]
        public List<KeyValuePair<InteractCondition, Interaction>> InteractToCondit { get; set; }
        [JsonIgnore]
        public List<KeyValuePair<ActCondition, Action>> ActToCondit { get; set; }



        public string strInteractionWithAll
        {
            get => InteractionWithAll.NameInMap();
            set { InteractionWithAll = value.MethodInMap<PhisicalObject.Interaction>(); }
        }
        public string strActionAlways
        {
            get => ActionAlways.NameInMap();
            set { ActionAlways = value.MethodInMap<PhisicalObject.Action>(); }
        }

        public List<KeyValuePair<string, string>> strInteractToCondit
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
        public List<KeyValuePair<string, string>> strActToCondit
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


        public RigidBody()
        {
            mp = new MaterialPoint();
            rc = new RotatCharacter();

            NullPhDelegate.SetPhDelegateValue(this);
        }

        public RigidBody(MaterialPoint MP, RotatCharacter RC)
        {
            mp = MP;
            rc = RC;

            NullPhDelegate.SetPhDelegateValue(this);
        }

        public PhisicalObject Move(double timeInSec)
        {
            mp.Move(timeInSec);

            rc.Move(timeInSec);

            return this;
        }

        public PhisicalObject AddLinearAcceleration(Coord a, double timeInSec)
        {
            mp.AddLinearAcceleration(a, timeInSec);
            return this;
        }

        public PhisicalObject ActForce(Coord F, double timeInSec)
        {
            mp.ActForce(F, timeInSec);
            return this;
        }

        public PhisicalObject AddImpulse(Coord p)
        {
            mp.AddImpulse(p);
            return this;
        }

        public PhisicalObject AddAngularAcceleration(double e, double timeInSec)
        {
            rc.AddAcceleration(e, timeInSec);
            return this;
        }

        public PhisicalObject ActMomentOfForce(double M, double timeInSec)
        {
            rc.ActMomentOfForce(M, timeInSec);
            return this;
        }

        public PhisicalObject AddMomentOfImpulse(double L)
        {
            rc.AddMomentOfImpulse(L);
            return this;
        }
    }
}
