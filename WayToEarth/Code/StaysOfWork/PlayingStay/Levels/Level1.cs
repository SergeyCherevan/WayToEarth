using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.StaysOfWork.Levels
{
    class Level1 : PlayingStay
    {
        public override int NumberOfLevel { get { return 1; } set { } }

        override public List<GameObject> SetValueOfGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();


            centerPlanet = new Planet();
            gameObjects.Add(centerPlanet);

            centerPlanet.image = MainWindow.window.Planet;



            rocket = new Rocket();
            gameObjects.Add(rocket);

            rocket.image = MainWindow.window.Rocket;

            rocket.Y = -400;
            rocket.Angle = 0;

            rocket.phisObj.speed = Gravitation.CosmicSpeeds(rocket.phisObj, centerPlanet.phisObj, 1);


            fire = new ReactiveGases(rocket);

            fire.image = MainWindow.window.Fire;

            gameObjects.Add(fire);


            playingBorder = new PlayingBorder(centerPlanet, rocket);

            playingBorder.image = MainWindow.window.Border;

            gameObjects.Add(playingBorder);



            meteors = SetValueOfMeteors();

            gameObjects = gameObjects.Concat(meteors).ToList();



            return gameObjects;
        }

        public List<Meteor> SetValueOfMeteors()
        {
            List<Meteor> meteors = new List<Meteor>();

            Random r = new Random();

            for (int i = 0; i < countOfMeteors; i++)
            {
                meteors.Add(new Meteor());

                meteors[i].image = MainWindow.Clone(MainWindow.window.Meteor);
                MainWindow.window.PlayingCanvas.Children.Add(meteors[i].image);


                double rOrbitrocket = Complex.Abs(rocket.phisObj.coord - centerPlanet.phisObj.coord);

                Complex coord = Complex.FromPolarCoordinates(
                        r.Next(
                            (int)rOrbitrocket + 150,
                            (int)rOrbitrocket + 1500
                        ),
                        r.NextDouble() * 2 * Math.PI
                    );

                (meteors[i] as Meteor).phisObj.coord = coord;

                int dirOfRotat = r.NextDouble() < 0.5 ? 1 : -1;

                (meteors[i] as Meteor).phisObj.speed = Gravitation.CosmicSpeeds((meteors[i] as Meteor).phisObj, centerPlanet.phisObj, dirOfRotat) + (r.NextDouble() - 0.5) * 2;
            }

            return meteors;
        }


        override public void SetActionsAndInteractions(List<GameObject> gameObjects)
        {
            rocket.ActionAlwaysBeforeIntract += Rocket.JetTransEngineOperation;
            rocket.ActionAlwaysBeforeIntract += Rocket.JetRotatEngineOperation;

            rocket.ActionAlwaysAfterPhisic += Rocket.ResetValueOfEngine;

            rocket.phisObj.ActionAlways += RocketBody.JetTransEngineOperation;
            rocket.phisObj.ActionAlways += RocketBody.JetRotatEngineOperation;

            rocket.InteractToCondit.Add(new KeyValuePair<InteractCondition, Interaction>(Rocket.RocketIsCollided, Rocket.RocketCollide));


            fire.ActionAlwaysAfterIntract += ReactiveGases.UpdateIsVisio;

            foreach (GameObject go in gameObjects)
            {
                go.InteractToCondit.Add(new KeyValuePair<InteractCondition, Interaction>(Collision.isCollided, Collision.ObjectsCollision));
            }

            gModel.CollisionTypes.Add(null, null, Collision.CollisionsOfAll);
            gModel.CollisionTypes.Add(typeof(Planet), null, Collision.CollisionWhithPlanet);
            gModel.CollisionTypes.Add(typeof(Planet), typeof(Rocket), Collision.CollisionOfPlanetWhithRocket);
        }
    }
}
