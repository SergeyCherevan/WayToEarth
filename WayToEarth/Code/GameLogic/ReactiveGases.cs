using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;

namespace WayToEarth.GameLogic
{
    class ReactiveGases : VisioObject
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

        public override Complex Coord { get { return rocket?.Coord ?? 0; } set { } }

        public override double X { get { return rocket?.X ?? 0; } set { } }

        public override double Y { get { return rocket?.Y ?? 0; } set { } }

        public override double Angle { get { return rocket?.Angle ?? 0; } set { } }

        public static void UpdateIsVisio(GameObject f, double timeInSec)
        {
            ReactiveGases fire = (ReactiveGases)f;

            fire.isVisible = (fire.rocket.translationEngine != Rocket.TranslationEngine.Off && fire.rocket.isValid);
        }

        static ReactiveGases()
        {
            MethodNameMap<GameObject.Action>.AddMethod(UpdateIsVisio);
        }
    }
}
