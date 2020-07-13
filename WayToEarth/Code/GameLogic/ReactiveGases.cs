using Newtonsoft.Json;
using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    public class ReactiveGases : VisioObject
    {
        [JsonIgnore]
        public Rocket rocket;

        public ReactiveGases() : base()
        {
            rocket = null;
        }

        public ReactiveGases(Rocket R) : base()
        {
            rocket = R;
        }

        public override Coord Coord { get { return rocket?.Coord ?? new Coord(); } set { } }

        public override double Angle { get { return rocket?.Angle ?? 0; } set { } }

        public static void UpdateIsVisio(GameObject f, double timeInSec)
        {
            ReactiveGases fire = (ReactiveGases)f;

            fire.isVisible = (fire.rocket.translationEngine != Rocket.TranslationEngine.Off && fire.rocket.isValid);
        }
    }
}
