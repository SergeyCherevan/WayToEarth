using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WayToEarth.Phisic
{
    class RotatCharacter
    {
        public double angle;
        public double angulVel;
        public double inertMoment;


        public RotatCharacter()
        {
            angle = angulVel = 0;
            inertMoment = 1;
        }

        public RotatCharacter(double Angle, double AngularVelocity, double InertionMoment)
        {
            angle = Angle;
            angulVel = AngularVelocity;
            inertMoment = InertionMoment;
        }

        public RotatCharacter Move(double timeInSec)
        {
            angle += angulVel * timeInSec;
            return this;
        }

        public RotatCharacter AddAcceleration(double e, double timeInSec)
        {
            angulVel += e * timeInSec;
            return this;
        }

        public RotatCharacter ActMomentOfForce(double M, double timeInSec)
        {
            AddAcceleration(M / inertMoment, timeInSec);
            return this;
        }

        public RotatCharacter AddMomentOfImpulse(double L)
        {
            angulVel += L / inertMoment;
            return this;
        }
    }
}
