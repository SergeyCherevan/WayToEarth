using System;

namespace WayToEarth.Phisic
{
    static public class Gravitation
    {
        public static double GravityConst = 6.67e-11;

        public static Coord NewtonForce(PhisicalObject m1, PhisicalObject m2)
        {
            return GravityConst * m1.mass * m2.mass /
                Math.Pow((m2.coord - m1.coord).polarR, 3)
                                                                * (m2.coord - m1.coord);
        }

        public static void GravitationalInteraction(PhisicalObject m1, PhisicalObject m2, double timeInSec)
        {
            m1.ActForce(Gravitation.NewtonForce(m1, m2), timeInSec);
        }

        public static Coord CosmicSpeeds(PhisicalObject m1, PhisicalObject m2, int number)
        {
            double V = Math.Sqrt(
                Math.Abs(number) *
                                GravityConst * m2.mass /
                                (m2.coord - m1.coord).polarR
                );


            Coord Speed = Coord.FromPolar(
                    V * number / Math.Abs(number),
                    (m2.coord - m1.coord).polarAngle - Math.PI/2
                );

            return Speed + m2.speed;
        }
    }
}
