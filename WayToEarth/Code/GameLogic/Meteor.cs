using System;
using System.Collections.Generic;
using System.Text;
using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    class Meteor : PhisicSimulatedGameObj
    {
        override public double Angle
        {
            get
            {
                return phisObj.speed.polarAngle / Math.PI * 180;
            }
            set { }
        }

        public override double Radius { get { return image?.ActualHeight ?? 0; } }

        public Meteor() : base()
        {
            phisObj = new MaterialPoint();

            phisObj.mass = 1e+4;
        }
    }
}
