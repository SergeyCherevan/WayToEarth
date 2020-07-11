﻿using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    class VisioObject : GameObject
    {
        override public Coord Coord { get; set; }

        override public double Angle { get; set; }

        override public bool isVisible { get; set; }

        public VisioObject() : base()
        {
            isVisible = false;
        }

    }
}
