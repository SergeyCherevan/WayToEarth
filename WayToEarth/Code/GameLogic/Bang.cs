using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    public class Bang : VisioObject
    {
        double Time;

        public Bang() : base()
        {
            isVisible = true;

            Coord = new Coord();

            Time = 20;

            ActionAlwaysBeforeIntract += Burning;
        }

        public static void Burning(GameObject go, double timeInSec)
        {
            Bang bang = (Bang)go;

            bang.Angle += 5;

            bang.Time -= timeInSec;

            if (bang.Time <= 0)
            {
                bang.isValid = false;

                bang.isVisible = false;
            }
        }
    }
}
