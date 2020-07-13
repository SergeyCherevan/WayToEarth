using Newtonsoft.Json;
using System;
using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    public class PhisicSimulatedGameObj : GameObject
    {

        public PhisicalObject phisObj;

        [JsonIgnore] 
        override public Coord Coord { get { return phisObj.coord; } set { phisObj.coord = value; } }

        [JsonIgnore]
        override public double Angle
        {
            get {
                return 90 + phisObj.angle * 180 / Math.PI;
            } 
            set {
                phisObj.angle = (value - 90) * Math.PI / 180;
            } 
        }

        public override bool isVisible { get { return isValid; } set { } }

        public PhisicSimulatedGameObj() : base()
        {
            isVisible = true;
        }

    }
}
