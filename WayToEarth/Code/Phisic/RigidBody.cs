using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using WayToEarth.Phisic;
using static WayToEarth.Phisic.PhisicalObject;

namespace WayToEarth
{
    class RigidBody : PhisicalObject
    {
        public MaterialPoint mp;

        public RotatCharacter rc;

        public Complex coord { get { return mp.coord; } set { mp.coord = value; } }
        public Complex speed { get { return mp.speed; } set { mp.speed = value; } }
        public double mass { get { return mp.mass; } set { mp.mass = value; } }

        public double angle { get { return rc.angle; } set { rc.angle = value; } }
        public double angulVel { get { return rc.angulVel; } set { rc.angulVel = value; } }
        public double inertMoment { get { return rc.inertMoment; } set { rc.inertMoment = value; } }

        public double x { get { return mp.x; } set { mp.x = value; } }
        public double y { get { return mp.y; } set { mp.y = value; } }
        public double Vx { get { return mp.Vx; } set { mp.Vx = value; } }
        public double Vy { get { return mp.Vy; } set { mp.Vy = value; } }



        public phInteraction InteractionWithAll { get; set; }
        public phAction ActionAlways { get; set; }

        public List<KeyValuePair<InteractCondition, phInteraction>> InteractToCondit { get; set; }
        public List<KeyValuePair<ActCondition, phAction>> ActToCondit { get; set; }



        public RigidBody()
        {
            mp = new MaterialPoint();
            rc = new RotatCharacter();

            NullPhDelegate.SetPhDelegateValue(this);
        }

        public PhisicalObject Move(double timeInSec)
        {
            mp.Move(timeInSec);

            rc.Move(timeInSec);

            return this;
        }

        public PhisicalObject AddLinearAcceleration(Complex a, double timeInSec)
        {
            mp.AddLinearAcceleration(a, timeInSec);
            return this;
        }

        public PhisicalObject ActForce(Complex F, double timeInSec)
        {
            mp.ActForce(F, timeInSec);
            return this;
        }

        public PhisicalObject AddImpulse(Complex p)
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
