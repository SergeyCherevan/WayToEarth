using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace WayToEarth.GameLogic
{
    class VisioObject : GameObject
    {
        override public Complex Coord { get; set; }

        override public double X { get; set; }

        override public double Y { get; set; }

        override public double Angle { get; set; }

        override public bool isVisible { get; set; }

        public VisioObject() : base()
        {
            isVisible = false;
        }

    }
}
