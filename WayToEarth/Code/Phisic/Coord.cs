using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace WayToEarth.Phisic
{
    class Coord
    {
        public double x { get; set; }
        public double y { get; set; }

        public double polarAngle
        {
            get => Math.Atan2(y, x);
            set
            {
                x = polarR * Math.Cos(value);
                y = polarR * Math.Sin(value);
            }
        }

        public double polarR
        {
            get => Math.Sqrt(x * x + y * y);
            set
            {
                x = value * Math.Cos(polarAngle);
                y = value * Math.Sin(polarAngle);
            }
        }

        public Coord() { x = y = 0; }

        public Coord(double X, double Y)
        {
            x = X; y = Y;
        }

        public static Coord FromPolar(double R, double angle)
        {
            return new Coord(
                    R * Math.Cos(angle),
                    R * Math.Sin(angle)
                );
        }

        public static Coord operator +(Coord c1, Coord c2)
        {
            return new Coord(c1.x + c2.x, c1.y + c2.y);
        }

        public static Coord operator -(Coord c1, Coord c2)
        {
            return new Coord(c1.x - c2.x, c1.y - c2.y);
        }

        public static Coord operator *(Coord c1, double d)
        {
            return new Coord(c1.x * d, c1.y * d);
        }

        public static Coord operator *(double d, Coord c1)
        {
            return new Coord(c1.x * d, c1.y * d);
        }

        public static Coord operator /(Coord c1, double d)
        {
            return new Coord(c1.x / d, c1.y / d);
        }
    }
}
