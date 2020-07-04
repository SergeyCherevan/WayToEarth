using System;
using System.Collections.Generic;
using System.Text;
using WayToEarth.GameLogic;

namespace WayToEarth.GameLogic
{
    class Bang : VisioObject
    {
        double Time;

        public Bang() : base()
        {
            isVisible = true;

            Time = 10;

            ActionAlwaysBeforeIntract += Burning;
        }

        public static void Burning(GameObject go, double timeInSec)
        {
            Bang bang = (Bang)go;

            bang.Time -= timeInSec;

            if (bang.Time <= 0)
            {
                bang.isValid = false;

                bang.isVisible = false;
            }
        }

        static Bang()
        {
            gActionNameMap.AddMethod(Burning);
        }
    }
}
