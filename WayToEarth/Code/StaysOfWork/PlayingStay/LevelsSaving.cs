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

            centralObject = gModel.rocket;


            SetActionsAndInteractionsL(gameObjects);

            StreamWriter file = new StreamWriter($@"C:\Users\chere\Source\Repos\SergeyCherevan\WayToEarth\WayToEarth\Code\JSON\Level {level}.json", false);

            var jsonSerializer = new JsonSerializer();
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
            int countOfMeteors = 350;

            List<GameObject> gameObjects = new List<GameObject>();


            gModel.centerPlanet = new Planet();
            gModel.centerPlanet.phisObj.mass = 1e+14;
            gameObjects.Add(gModel.centerPlanet);

            gModel.centerPlanet.ImageName = "Planet";



            gModel.rocket = new Rocket();
            gameObjects.Add(gModel.rocket);

            gModel.rocket.ImageName = "Rocket";

            gModel.rocket.Y = -400;
            gModel.rocket.Angle = 0;

            gModel.rocket.phisObj.speed = Gravitation.CosmicSpeeds(gModel.rocket.phisObj, gModel.centerPlanet.phisObj, 1);


            gModel.fire = new ReactiveGases(gModel.rocket);

            gModel.fire.ImageName = "Fire";

            gameObjects.Add(gModel.fire);


            gModel.playingBorder = new PlayingBorder(gModel.centerPlanet, gModel.rocket);

            gModel.playingBorder.ImageName = "Border";

            gameObjects.Add(gModel.playingBorder);



            gModel.meteors = SetValueOfMeteorsL(countOfMeteors);

            gameObjects = gameObjects.Concat(gModel.meteors).ToList();



            return gameObjects;
        }

        public List<GameObject> SetValueOfGameObjectsL2()
        {
            int countOfPlanets = 5;

            List<GameObject> gameObjects = new List<GameObject>();

            int countOfMeteors = 350;


            gModel.centerPlanet = new Planet();
            gameObjects.Add(gModel.centerPlanet);
            gModel.centerPlanet.phisObj.angulVel = -2 * Math.PI / 500;
            gModel.centerPlanet.phisObj.mass = 1e+14;

            gModel.centerPlanet.ImageName = "CenterStar";



            gModel.planets = new List<Planet>();
            for (int i = 0; i < countOfPlanets; i++)
            {
                gModel.planets.Add(new Planet());
                gameObjects.Add(gModel.planets[i]);

                gModel.planets[i].ImageName = "SmallPlanet";

                gModel.planets[i].phisObj.mass = gModel.centerPlanet.phisObj.mass / 1000;
                gModel.planets[i].phisObj.coord = gModel.centerPlanet.phisObj.coord + Coord.FromPolar(500 + 400, 2 * Math.PI / countOfPlanets * i);
                gModel.planets[i].phisObj.speed = Gravitation.CosmicSpeeds(gModel.planets[i].phisObj, gModel.centerPlanet.phisObj, 1);
            }



            gModel.rocket = new Rocket();
            gameObjects.Add(gModel.rocket);

            gModel.rocket.ImageName = "Rocket";

            gModel.rocket.Y = -400;
            gModel.rocket.Angle = 0;

            gModel.rocket.phisObj.speed = Gravitation.CosmicSpeeds(gModel.rocket.phisObj, gModel.centerPlanet.phisObj, 1);


            gModel.fire = new ReactiveGases(gModel.rocket);

            gModel.fire.ImageName = "Fire";

            gameObjects.Add(gModel.fire);


            gModel.playingBorder = new PlayingBorder(gModel.centerPlanet, gModel.rocket);

            gModel.playingBorder.ImageName = "Border";

            gameObjects.Add(gModel.playingBorder);



            gModel.meteors = SetValueOfMeteorsL(countOfMeteors);

            gameObjects = gameObjects.Concat(gModel.meteors).ToList();



            return gameObjects;
        }

        public List<GameObject> SetValueOfGameObjectsL3()
        {
            int countOfPlanets = 5;

            List<GameObject> gameObjects = new List<GameObject>();

            int countOfMeteors = 350;


            gModel.centerPlanet = new Planet();
            gameObjects.Add(gModel.centerPlanet);
            gModel.centerPlanet.phisObj.angulVel = 2 * Math.PI / 500;
            gModel.centerPlanet.phisObj.mass = 1e+14;

            gModel.centerPlanet.ImageName = "CenterStar";



            gModel.planets = new List<Planet>();
            for (int i = 0; i < countOfPlanets; i++)
            {
                gModel.planets.Add(new Planet());
                gameObjects.Add(gModel.planets[i]);

                gModel.planets[i].ImageName = "Planet";

                gModel.planets[i].phisObj.mass = gModel.centerPlanet.phisObj.mass / 100;
                gModel.planets[i].phisObj.coord = gModel.centerPlanet.phisObj.coord + Coord.FromPolar(500 + 400, 2 * Math.PI / countOfPlanets * i);
                gModel.planets[i].phisObj.speed = Gravitation.CosmicSpeeds(gModel.planets[i].phisObj, gModel.centerPlanet.phisObj, 1);
            }



            gModel.rocket = new Rocket();
            gameObjects.Add(gModel.rocket);

            gModel.rocket.ImageName = "Rocket";

            gModel.rocket.Y = -400;
            gModel.rocket.Angle = 0;

            gModel.rocket.phisObj.speed = Gravitation.CosmicSpeeds(gModel.rocket.phisObj, gModel.centerPlanet.phisObj, 1);


            gModel.fire = new ReactiveGases(gModel.rocket);

            gModel.fire.ImageName = "Fire";

            gameObjects.Add(gModel.fire);


            gModel.playingBorder = new PlayingBorder(gModel.centerPlanet, gModel.rocket);

            gModel.playingBorder.ImageName = "Border";

            gameObjects.Add(gModel.playingBorder);



            gModel.meteors = SetValueOfMeteorsL(countOfMeteors);

            gameObjects = gameObjects.Concat(gModel.meteors).ToList();



            return gameObjects;
        }

        public List<Meteor> SetValueOfMeteorsL(int countOfMeteors)
        {
            gModel.meteors = new List<Meteor>();

            Random r = new Random();

            for (int i = 0; i < countOfMeteors; i++)
            {
                gModel.meteors.Add(new Meteor());

                gModel.meteors[i].ImageName = "Meteor";


                gModel.meteors[i].phisObj.mass = 1000;

                double rOrbitRocket = (gModel.rocket.phisObj.coord - gModel.centerPlanet.phisObj.coord).polarR;

                Coord coord = Coord.FromPolar(
                        r.Next(
                            (int)rOrbitRocket + 150,
                            (int)rOrbitRocket + 1500
                        ),
                        r.NextDouble() * 2 * Math.PI
                    );

                gModel.meteors[i].phisObj.coord = coord;

                int dirOfRotat = r.NextDouble() < 0.5 ? 1 : -1;

                gModel.meteors[i].phisObj.speed = Gravitation.CosmicSpeeds(gModel.meteors[i].phisObj, gModel.centerPlanet.phisObj, dirOfRotat);
                gModel.meteors[i].phisObj.speed +=
                    Coord.FromPolar(
                            r.NextDouble() * gModel.meteors[i].phisObj.speed.polarR * 0.5,
                            r.NextDouble() * 2 * Math.PI
                        );
            }

            return gModel.meteors;
        }

        public void SetActionsAndInteractionsL(List<GameObject> gameObjects)
        {
            gModel.rocket.ActionAlwaysBeforeIntract += Rocket.JetTransEngineOperation;
            gModel.rocket.ActionAlwaysBeforeIntract += Rocket.JetRotatEngineOperation;

            gModel.rocket.ActionAlwaysAfterPhisic += Rocket.ResetValueOfEngine;

            gModel.rocket.phisObj.ActionAlways += RocketBody.JetTransEngineOperation;
            gModel.rocket.phisObj.ActionAlways += RocketBody.JetRotatEngineOperation;

            gModel.rocket.InteractToCondit.Add(new KeyValuePair<InteractCondition, Interaction>(Rocket.RocketIsCollided, Rocket.RocketCollide));


            gModel.fire.ActionAlwaysAfterIntract += ReactiveGases.UpdateIsVisio;

            foreach (GameObject go in gameObjects)
            {
                go.InteractToCondit.Add(new KeyValuePair<InteractCondition, Interaction>(Collision.isCollided, Collision.ObjectsCollision));
            }

            gModel.CollisionTypes.Add(null, null, Collision.CollisionsOfAll);
            gModel.CollisionTypes.Add(typeof(Planet), null, Collision.CollisionWhithPlanet);
            gModel.CollisionTypes.Add(typeof(Rocket), null, Collision.CollisionWhithRocket);
            gModel.CollisionTypes.Add(typeof(Planet), typeof(Rocket), Collision.CollisionOfPlanetWhithRocket);
        }
    }
}
