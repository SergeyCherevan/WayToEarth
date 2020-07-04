using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using static WayToEarth.Phisic.PhisicalObject;

namespace WayToEarth.Phisic
{
    class MaterialPoint : PhisicalObject
    {
        public Complex coord { get; set; }
        public Complex speed { get; set; }
        public double mass { get; set; }

        public double angle { get { return 0; } set { } }
        public double angulVel { get { return 0; } set { } }
        public double inertMoment { get { return 0; } set { } }

        public double x { get { return coord.Real; } set { coord = new Complex(value, coord.Imaginary); } }
        public double y { get { return coord.Imaginary; } set { coord = new Complex(coord.Real, value); } }
        public double Vx { get { return speed.Real; } set { speed = new Complex(value, speed.Imaginary); } }
        public double Vy { get { return speed.Imaginary; } set { speed = new Complex(speed.Real, value); } }



        public phInteraction InteractionWithAll { get; set; }
        public phAction ActionAlways { get; set; }

        public List<KeyValuePair<InteractCondition, phInteraction>> InteractToCondit { get; set; }
        public List<KeyValuePair<ActCondition, phAction>> ActToCondit { get; set; }



        public MaterialPoint()
        {
            coord = speed = new Complex();
            mass = 1;

            NullPhDelegate.SetPhDelegateValue(this);
        }

        public MaterialPoint(Complex Coord, Complex Speed, double M)
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
        
        public PhisicalObject AddLinearAcceleration(Complex a, double timeInSec)
        {
            speed += a * timeInSec;
            return this;
        }

        public PhisicalObject ActForce(Complex F, double timeInSec)
        {
            AddLinearAcceleration(F/mass, timeInSec);
            return this;
        }

        public PhisicalObject AddImpulse(Complex p)
        {
            speed += p / mass;
            return this;
        }

        public PhisicalObject AddAngularAcceleration(double e, double timeInSec) { return this; }

        public PhisicalObject ActMomentOfForce(double M, double timeInSec) { return this; }

        public PhisicalObject AddMomentOfImpulse(double L) { return this; }
    }
}
