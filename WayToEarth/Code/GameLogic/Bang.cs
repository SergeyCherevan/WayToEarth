using System;
using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    public class Bang : VisioObject
    {
        double Time;

        double angulVel;

        public Bang() : base()
        {
            isVisible = true;

            Coord = new Coord();

            Time = 20;

            while(Math.Abs(angulVel) < 2) angulVel = new Random().Next(-7, 8);

            ActionAlwaysBeforeIntract += Burning;
        }

        public static void Burning(GameObject go, double timeInSec)
        {
            Bang bang = go as Bang;

            bang.Angle += bang.angulVel;

            bang.Time -= timeInSec;

            if (bang.Time <= 0)
            {
                bang.isValid = false;

                bang.isVisible = false;
            }
        }
    }
}
