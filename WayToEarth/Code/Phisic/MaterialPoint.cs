using Newtonsoft.Json;
using System.Collections.Generic;
using WayToEarth.GameLogic;
using WayToEarth.NameMaps;
using static WayToEarth.Phisic.PhisicalObject;
using Action = WayToEarth.Phisic.PhisicalObject.Action;

namespace WayToEarth.Phisic
{
    public class MaterialPoint : PhisicalObject
    {
        public MaterialPoint mp
        {
            get => this;
            set
            {
                coord = value.coord;
                speed = value.speed;
                mass = value.mass;
            }
        }

        public Coord coord { get; set; }
        public Coord speed { get; set; }
        public double mass { get; set; }

        [JsonIgnore] double x { get => coord.x; set { coord.x = value; } }
        [JsonIgnore] double y { get => coord.y; set { coord.y = value; } }
        [JsonIgnore] double Vx { get => speed.x; set { speed.x = value; } }
        [JsonIgnore] double Vy { get => speed.y; set { speed.y = value; } }



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

        public string strInteractToCondit
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
        public string strActToCondit
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




        public MaterialPoint()
        {
            coord = new Coord();
            speed = new Coord();
            mass = 1;

            NullPhDelegate.SetPhDelegateValue(this);
        }

        public MaterialPoint(Coord Coord, Coord Speed, double M)
        {
            coord = Coord;
            speed = Speed;
            mass = M;

            NullPhDelegate.SetPhDelegateValue(this);
        }

        public PhisicalObject Move(double timeInSec)
        {
            coord += speed * timeInSec;
            return this;
        }

        public PhisicalObject AddLinearAcceleration(Coord a, double timeInSec)
        {
            speed += a * timeInSec;
            return this;
        }

        public PhisicalObject ActForce(Coord F, double timeInSec)
        {
            AddLinearAcceleration(F / mass, timeInSec);
            return this;
        }

        public PhisicalObject AddImpulse(Coord p)
        {
            speed += p / mass;
            return this;
        }
    }
}
