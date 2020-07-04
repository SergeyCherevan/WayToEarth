using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    public class PhisicSimulatedGameObj : GameObject
    {

        public PhisicalObject phisObj;

        override public Complex Coord { get { return phisObj.coord; } set { phisObj.coord = value; } }

        override public double X { get { return phisObj.x; } set { phisObj.x = value; } }

        override public double Y { get { return phisObj.y; } set { phisObj.y = value; } }

        override public double Angle
        {
            get {
                return 90 + phisObj.angle * 180 / Math.PI;
            } 
            set {
                phisObj.angle = (value - 90) * Math.PI / 180;
            } 
        }

        public override bool isVisible { get { return isValid; } set { } }

        public PhisicSimulatedGameObj() : base()
        {
            isVisible = true;
        }

    }
}
