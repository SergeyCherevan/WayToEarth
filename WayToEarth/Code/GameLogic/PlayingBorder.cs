using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;

namespace WayToEarth.GameLogic
{
    class PlayingBorder : VisioObject
    {
        [JsonIgnore]
        public Planet planet;
        [JsonIgnore]
        public Rocket rocket;

        public PlayingBorder() : base()
        {
            planet = null;

            rocket = null;

            ActToConditAfterIntract.Add(new KeyValuePair<ActCondition, gAction>(WentAbroad, GameModel.Win));
        }

        public PlayingBorder(Planet P, Rocket R) : base()
        {
            planet = P;

            rocket = R;

            ActToConditAfterIntract.Add(new KeyValuePair<ActCondition, gAction>(WentAbroad, GameModel.Win));
        }

        public override Complex Coord { get { return planet?.Coord ?? 0; } set { } }

        public override double X { get { return planet?.X ?? 0; } set { } }

        public override double Y { get { return planet?.Y ?? 0; } set { } }

        public override double Angle { get { return 0; } set { } }

        public override double Radius { get { return Width / 2; } }

        public override bool isVisible { get { return true; } set { } }

        public static bool WentAbroad(GameObject go, double timeInSec)
        {
            PlayingBorder border = (PlayingBorder)go;

            return Complex.Abs(border.Coord - border.rocket.Coord) >= border.Radius;
        }

        static PlayingBorder()
        {
            gConditActionNameMap.map.Add("WentAbroad", WentAbroad);
        }
    }
}
