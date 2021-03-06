﻿using System;

namespace WayToEarth.Phisic
{
    class RocketBody : RigidBody
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

        public double dImpulsPerSecond;
        public double dMomentumPerSecond;



        public RocketBody() : base()
        {
            translationEngine = 0;
            rotationEngine = 0;

            dImpulsPerSecond = 15;
            dMomentumPerSecond = Math.PI / 20;
        }



        public RocketBody(MaterialPoint mp, RotatCharacter rc) : base(mp, rc)
        {
            translationEngine = 0;
            rotationEngine = 0;

            dImpulsPerSecond = 15;
            dMomentumPerSecond = Math.PI / 20;
        }



        static public void JetTransEngineOperation(PhisicalObject o, double timeInSecond)
        {

            RocketBody rocket = (RocketBody)o;

            Coord dp = Coord.FromPolar(
                    rocket.dImpulsPerSecond * (int)rocket.translationEngine,
                    rocket.angle
                );

            rocket.AddImpulse(dp);
        }

        static public void JetRotatEngineOperation(PhisicalObject o, double timeInSecond)
        {
            RocketBody rocket = (RocketBody)o;

            rocket.AddMomentOfImpulse(
                    rocket.dMomentumPerSecond * (int)rocket.rotationEngine
                );
        }
    }
}
