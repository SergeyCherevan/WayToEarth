using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;

namespace WayToEarth.Phisic
{
    static class Gravitation
    {
        public static double GravityConst = 6.67 * Math.Pow(10, -11);

        public static Complex NewtonForce(PhisicalObject m1, PhisicalObject m2)
        {
            return GravityConst * m1.mass * m2.mass /
                Math.Pow(Complex.Abs(m2.coord - m1.coord), 3)
                                                                * (m2.coord - m1.coord);
        }

        public static void GravitationalInteraction(PhisicalObject m1, PhisicalObject m2, double timeInSec)
        {
            m1.ActForce(Gravitation.NewtonForce(m1, m2), timeInSec);
        }

        public static Complex CosmicSpeeds(PhisicalObject m1, PhisicalObject m2, int number)
        {
            double V = Math.Sqrt(
                Math.Abs(number) *
                                GravityConst * m2.mass /
                                Complex.Abs(m2.coord - m1.coord)
                );


            Complex Speed = Complex.FromPolarCoordinates(
                    V * number / Math.Abs(number),
                    (m2.coord - m1.coord).Phase - Math.PI/2
                );

            //MessageBox.Show(Speed.Real + ", " + Speed.Imaginary);

            return Speed + m2.speed;
        }
    }
}
