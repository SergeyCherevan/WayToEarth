using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using WayToEarth.GameLogic;
using static WayToEarth.Phisic.PhisicalObject;
using Action = WayToEarth.Phisic.PhisicalObject.Action;

namespace WayToEarth.Phisic
{
    class MaterialPoint : PhisicalObject
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

        double x { get => coord.x; set { coord.x = value; } }
        double y { get => coord.y; set { coord.y = value; } }
        double Vx { get => speed.x; set { speed.x = value; } }
        double Vy { get => speed.y; set { speed.y = value; } }



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

        /*public Coord coord { get; set; }
        public Coord speed { get; set; }
        public double mass { get; set; }

        public double angle { get { return 0; } set { } }
        public double angulVel { get { return 0; } set { } }
        public double inertMoment { get { return 0; } set { } }

        public double x { get { return coord.Real; } set { coord = new Coord(value, coord.Imaginary); } }
        public double y { get { return coord.Imaginary; } set { coord = new Coord(coord.Real, value); } }
        public double Vx { get { return speed.Real; } set { speed = new Coord(value, speed.Imaginary); } }
        public double Vy { get { return speed.Imaginary; } set { speed = new Coord(speed.Real, value); } }



        public Interaction InteractionWithAll { get; set; }
        public Action ActionAlways { get; set; }

        public List<KeyValuePair<InteractCondition, Interaction>> InteractToCondit { get; set; }
        public List<KeyValuePair<ActCondition, Action>> ActToCondit { get; set; }



        public MaterialPoint()
        {
            coord = speed = new Coord();
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
            AddLinearAcceleration(F/mass, timeInSec);
            return this;
        }

        public PhisicalObject AddImpulse(Coord p)
        {
            speed += p / mass;
            return this;
        }

        public PhisicalObject AddAngularAcceleration(double e, double timeInSec) { return this; }

        public PhisicalObject ActMomentOfForce(double M, double timeInSec) { return this; }

        public PhisicalObject AddMomentOfImpulse(double L) { return this; }*/
    }
}
