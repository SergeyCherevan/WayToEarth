using System;
using System.Collections.Generic;
using System.Text;
using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    class Planet : PhisicSimulatedGameObj
    {
        public override double Radius { get { return (image?.ActualHeight ?? 0) / 2; } }

        public Planet() : base()
        {
            phisObj = new RigidBody();

            phisObj.mass = 1e+14;

            Angle = 0;
        }
    }
}
