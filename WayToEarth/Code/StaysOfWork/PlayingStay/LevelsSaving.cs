using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.StaysOfWork
{
    public partial class PlayingStay
    {

        public void SaveLevel(int level)
        {
            gModel = new GameModel();

            List<GameObject> gameObjects =
                level == 1 ? SetValueOfGameObjectsL1() :
                level == 2 ? SetValueOfGameObjectsL2() : SetValueOfGameObjectsL3();

            gModel.RegisterListOfGameObjects(gameObjects);

            gModel.phModel.SetGravitationInteractive();

            centralObject = rocket;


            SetActionsAndInteractionsL(gameObjects);

            StreamWriter file = new StreamWriter($@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\bin\Debug\netcoreapp3.1\Level {level}.json", false);

            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializer.TypeNameHandling = TypeNameHandling.Auto;
            jsonSerializer.Formatting = Formatting.Indented;

            JsonWriter jsonWriter = new JsonTextWriter(file);
            jsonSerializer.Serialize(jsonWriter, gModel);

            jsonWriter.Close();
            file.Close();
        }

        public List<GameObject> SetValueOfGameObjectsL1()
        {
            List<GameObject> gameObjects = new List<GameObject>();


            centerPlanet = new Planet();
            gameObjects.Add(centerPlanet);

            centerPlanet.ImageName = "Planet";



            rocket = new Rocket();
            gameObjects.Add(rocket);

            rocket.ImageName = "Rocket";

            rocket.Y = -400;
            rocket.Angle = 0;

            rocket.phisObj.speed = Gravitation.CosmicSpeeds(rocket.phisObj, centerPlanet.phisObj, 1);


            fire = new ReactiveGases(rocket);

            fire.ImageName = "Fire";

            gameObjects.Add(fire);


            playingBorder = new PlayingBorder(centerPlanet, rocket);

            playingBorder.ImageName = "Border";

            gameObjects.Add(playingBorder);



            meteors = SetValueOfMeteorsL();

            gameObjects = gameObjects.Concat(meteors).ToList();



            return gameObjects;
        }

        public List<GameObject> SetValueOfGameObjectsL2()
        {
            int countOfPlanets = 5;

            List<GameObject> gameObjects = new List<GameObject>();

            countOfMeteors = 350;


            centerPlanet = new Planet();
            gameObjects.Add(centerPlanet);
            centerPlanet.phisObj.angulVel = -2 * Math.PI / 500;

            centerPlanet.ImageName = "CenterStar";



            planets = new List<Planet>();
            for (int i = 0; i < countOfPlanets; i++)
            {
                planets.Add(new Planet());
                gameObjects.Add(planets[i]);

                planets[i].ImageName = "SmallPlanet";

                planets[i].phisObj.mass = centerPlanet.phisObj.mass / 10000000;
                planets[i].phisObj.coord = centerPlanet.phisObj.coord + Coord.FromPolar(500 + 400, 2 * Math.PI / countOfPlanets * i);
                planets[i].phisObj.speed = Gravitation.CosmicSpeeds(planets[i].phisObj, centerPlanet.phisObj, 1);
            }



            rocket = new Rocket();
            gameObjects.Add(rocket);

            rocket.ImageName = "Rocket";

            rocket.Y = -400;
            rocket.Angle = 0;

            rocket.phisObj.speed = Gravitation.CosmicSpeeds(rocket.phisObj, centerPlanet.phisObj, 1);


            fire = new ReactiveGases(rocket);

            fire.ImageName = "Fire";

            gameObjects.Add(fire);


            playingBorder = new PlayingBorder(centerPlanet, rocket);

            playingBorder.ImageName = "Border";

            gameObjects.Add(playingBorder);



            meteors = SetValueOfMeteorsL();

            gameObjects = gameObjects.Concat(meteors).ToList();



            return gameObjects;
        }

        public List<GameObject> SetValueOfGameObjectsL3()
        {
            int countOfPlanets = 5;

            List<GameObject> gameObjects = new List<GameObject>();

            countOfMeteors = 350;


            centerPlanet = new Planet();
            gameObjects.Add(centerPlanet);
            centerPlanet.phisObj.angulVel = 2 * Math.PI / 500;

            centerPlanet.ImageName = "CenterStar";



            planets = new List<Planet>();
            for (int i = 0; i < countOfPlanets; i++)
            {
                planets.Add(new Planet());
                gameObjects.Add(planets[i]);

                planets[i].ImageName = "Planet";

                planets[i].phisObj.mass = centerPlanet.phisObj.mass / 100;
                planets[i].phisObj.coord = centerPlanet.phisObj.coord + Coord.FromPolar(500 + 400, 2 * Math.PI / countOfPlanets * i);
                planets[i].phisObj.speed = Gravitation.CosmicSpeeds(planets[i].phisObj, centerPlanet.phisObj, 1);
            }



            rocket = new Rocket();
            gameObjects.Add(rocket);

            rocket.ImageName = "Rocket";

            rocket.Y = -400;
            rocket.Angle = 0;

            rocket.phisObj.speed = Gravitation.CosmicSpeeds(rocket.phisObj, centerPlanet.phisObj, 1);


            fire = new ReactiveGases(rocket);

            fire.ImageName = "Fire";

            gameObjects.Add(fire);


            playingBorder = new PlayingBorder(centerPlanet, rocket);

            playingBorder.ImageName = "Border";

            gameObjects.Add(playingBorder);



            meteors = SetValueOfMeteorsL();

            gameObjects = gameObjects.Concat(meteors).ToList();



            return gameObjects;
        }

        public List<Meteor> SetValueOfMeteorsL()
        {
            List<Meteor> meteors = new List<Meteor>();

            Random r = new Random();

            for (int i = 0; i < countOfMeteors; i++)
            {
                meteors.Add(new Meteor());

                meteors[i].ImageName = "Meteor";


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

                (meteors[i] as Meteor).phisObj.speed = Gravitation.CosmicSpeeds((meteors[i] as Meteor).phisObj, centerPlanet.phisObj, dirOfRotat) + new Coord((r.NextDouble() - 0.5), (r.NextDouble() - 0.5));
            }

            return meteors;
        }

        public void SetActionsAndInteractionsL(List<GameObject> gameObjects)
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
