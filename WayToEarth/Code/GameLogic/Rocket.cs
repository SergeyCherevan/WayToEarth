using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WayToEarth.Phisic;

namespace WayToEarth.GameLogic
{
    class Rocket : PhisicSimulatedGameObj
    {

        public enum TranslationEngine
        {
            Back = -1,
            Off,
            Front
        }

        public enum RotationEngine
        {
            Left = -1,
            Off,
            Right
        }

        public TranslationEngine translationEngine;
        public RotationEngine rotationEngine;

        public double NormdImpulsPerSecond;
        public double NormdMomentumPerSecond;



        public Rocket() : base()
        {
            phisObj = new RocketBody();

            phisObj.mass = 100;

            phisObj.inertMoment = 100;

            phisObj.angulVel = 0;

            translationEngine = 0;
            rotationEngine = 0;

            NormdImpulsPerSecond = 10;
            NormdMomentumPerSecond = Math.PI / 10;
        }



        static public void JetTransEngineOperation(GameObject o, double timeInSecond)
        {
            Rocket rocket = (Rocket)o;

            (rocket.phisObj as RocketBody).translationEngine = (RocketBody.TranslationEngine)rocket.translationEngine;
        }

        static public void JetRotatEngineOperation(GameObject o, double timeInSecond)
        {
            Rocket rocket = (Rocket)o;

            (rocket.phisObj as RocketBody).rotationEngine = (RocketBody.RotationEngine)rocket.rotationEngine;
        }

        static public void ResetValueOfEngine(GameObject o, double timeInSecond)
        {
            Rocket rocket = (Rocket)o;

            rocket.translationEngine = 0;
            rocket.rotationEngine = 0;
            (rocket.phisObj as RocketBody).translationEngine = 0;
            (rocket.phisObj as RocketBody).rotationEngine = 0;
        }


        public static bool RocketIsCollided(GameObject go1, GameObject go2, double timeInSec)
        {
            if (go1 is Rocket)
            {
                if (go2 is VisioObject || !go2.isValid) return false;

                return Complex.Abs(go1.Coord - go2.Coord) < go1.Radius + go2.Radius;
            }
            else
            {
                if (go1 is VisioObject || !go1.isValid) return false;

                return Complex.Abs(go1.Coord - go2.Coord) < go1.Radius + go2.Radius;
            }

        }

        static public void RocketCollide(GameObject rocket, GameObject o, double timeInSecond)
        {
            GameModel.Loose(rocket, timeInSecond);
        }

        static Rocket()
        {
            MethodNameMap<GameObject.Action>.AddMethod(JetRotatEngineOperation);
            MethodNameMap<GameObject.Action>.AddMethod(JetTransEngineOperation);
            MethodNameMap<GameObject.Action>.AddMethod(ResetValueOfEngine);

            MethodNameMap<GameObject.InteractCondition>.AddMethod(RocketIsCollided);

            MethodNameMap<GameObject.Interaction>.AddMethod(RocketCollide);
        }
    }
}
