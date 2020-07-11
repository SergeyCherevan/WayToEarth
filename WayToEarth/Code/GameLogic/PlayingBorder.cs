using Newtonsoft.Json;
using System.Collections.Generic;
using WayToEarth.Phisic;

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

            ActToConditAfterIntract.Add(new KeyValuePair<ActCondition, Action>(WentAbroad, GameModel.Win));
        }

        public PlayingBorder(Planet P, Rocket R) : base()
        {
            planet = P;

            rocket = R;

            ActToConditAfterIntract.Add(new KeyValuePair<ActCondition, Action>(WentAbroad, GameModel.Win));
        }

        public override Coord Coord { get { return planet?.Coord ?? new Coord(); } set { } }

        public override double Angle { get { return 0; } set { } }

        public override double Radius { get { return Width / 2; } }

        public override bool isVisible { get { return true; } set { } }

        public static bool WentAbroad(GameObject go, double timeInSec)
        {
            PlayingBorder border = (PlayingBorder)go;

            return (border.Coord - border.rocket.Coord).polarR >= border.Radius;
        }
    }
}
