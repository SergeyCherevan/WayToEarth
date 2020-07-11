using System;
using System.Collections.Generic;
using System.Linq;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.StaysOfWork.Levels
{
    class Level3 : PlayingStay
    {
        public override int NumberOfLevel { get { return 3; } set { } }

        List<Planet> planets;
        int countOfPlanets = 5;


        override public List<GameObject> SetValueOfGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            countOfMeteors = 350;


            centerPlanet = new Planet();
            gameObjects.Add(centerPlanet);
            centerPlanet.phisObj.angulVel = 2 * Math.PI / 500;

            centerPlanet.image = MainWindow.window.CenterStar;



            planets = new List<Planet>();
            for (int i = 0; i < countOfPlanets; i++)
            {
                planets.Add(new Planet());
                gameObjects.Add(planets[i]);

                planets[i].image = MainWindow.Clone(MainWindow.window.SmallPlanet);
                MainWindow.window.PlayingCanvas.Children.Add(planets[i].image);

                planets[i].phisObj.mass = centerPlanet.phisObj.mass / 100;
                planets[i].phisObj.coord = centerPlanet.phisObj.coord + Coord.FromPolar(500 + 400, 2 * Math.PI / countOfPlanets * i);
                planets[i].phisObj.speed = Gravitation.CosmicSpeeds(planets[i].phisObj, centerPlanet.phisObj, 1);
            }



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


                double rOrbitrocket = (rocket.phisObj.coord - centerPlanet.phisObj.coord).polarR;

                Coord coord = Coord.FromPolar(
                        r.Next(
                            (int)rOrbitrocket + 150,
                            (int)rOrbitrocket + 1500
                        ),
                        r.NextDouble() * 2 * Math.PI
                    );

                (meteors[i] as Meteor).phisObj.coord = coord;

                int dirOfRotat = r.NextDouble() < 0.5 ? 1 : -1;

                (meteors[i] as Meteor).phisObj.speed = Gravitation.CosmicSpeeds((meteors[i] as Meteor).phisObj, centerPlanet.phisObj, dirOfRotat) + new Coord((r.NextDouble() - 0.5) * 2, 0);
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
