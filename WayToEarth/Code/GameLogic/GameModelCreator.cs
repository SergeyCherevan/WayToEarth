using System;
using System.Collections.Generic;
using System.Linq;

using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.GameLogic
{
    public class GameModelCreator
    {
        static public GameModel Create(int levelNum)
        {
            GameModel model = new GameModel();

            switch (levelNum)
            {
                case 1:
                case 2:
                case 3:
                    SetValueOfGameObjectsForLevel123(levelNum, model);
                    break;
            }                

            model.RegisterListOfGameObjects(model.gObjects);

            model.phModel.SetGravitationInteractive();


            SetActionsAndInteractions(model);

            return model;
        }

        static public void SetValueOfGameObjectsForLevel123(int levelNum, GameModel model)
        {
            int countOfPlanets = 5;
            int countOfMeteors = 350;

            model.gObjects = new List<GameObject>();


            model.centerPlanet = new Planet();
            model.centerPlanet.phisObj.mass = 1e+14;
            model.gObjects.Add(model.centerPlanet);

            if (levelNum == 1)
                model.centerPlanet.ImageName = "Planet";
            else
                model.centerPlanet.ImageName = "CenterStar";




            model.planets = new List<Planet>();
            if(levelNum > 1)
                for (int i = 0; i < countOfPlanets; i++)
                {
                    model.planets.Add(new Planet());
                    model.gObjects.Add(model.planets[i]);

                    model.planets[i].ImageName = "SmallPlanet";

                    if(levelNum == 2)
                        model.planets[i].phisObj.mass = model.centerPlanet.phisObj.mass / 1000;
                    else
                        model.planets[i].phisObj.mass = model.centerPlanet.phisObj.mass / 100;

                    model.planets[i].phisObj.coord = model.centerPlanet.phisObj.coord + Coord.FromPolar(500 + 400, 2 * Math.PI / countOfPlanets * i);
                    model.planets[i].phisObj.speed = Gravitation.CosmicSpeeds(model.planets[i].phisObj, model.centerPlanet.phisObj, 1);
                }



            model.rocket = new Rocket();
            model.gObjects.Add(model.rocket);

            model.rocket.ImageName = "Rocket";

            model.rocket.Y = -400;
            model.rocket.Angle = 0;

            model.rocket.phisObj.speed = Gravitation.CosmicSpeeds(model.rocket.phisObj, model.centerPlanet.phisObj, 1);


            model.fire = new ReactiveGases(model.rocket);

            model.fire.ImageName = "Fire";

            model.gObjects.Add(model.fire);


            model.playingBorder = new PlayingBorder(model.centerPlanet, model.rocket);

            model.playingBorder.ImageName = "Border";

            model.gObjects.Add(model.playingBorder);



            SetValueOfMeteorsForLevels123(model, countOfMeteors);

            model.gObjects = model.gObjects.Concat(model.meteors).ToList();

        }

        static public List<Meteor> SetValueOfMeteors
            (int levelNum, GameModel model, int countOfMeteors)
        {
            if (1 <= levelNum && levelNum <= 3)
                return SetValueOfMeteorsForLevels123(model, countOfMeteors);

            return null;
        }

        static public List<Meteor> SetValueOfMeteorsForLevels123 (GameModel model, int countOfMeteors)
        {
            model.meteors = new List<Meteor>();

            Random r = new Random();

            for (int i = 0; i < countOfMeteors; i++)
            {
                model.meteors.Add(new Meteor());

                model.meteors[i].ImageName = "Meteor";


                model.meteors[i].phisObj.mass = 1000;

                double rOrbitRocket = (model.rocket.phisObj.coord - model.centerPlanet.phisObj.coord).polarR;

                Coord coord = Coord.FromPolar(
                        r.Next(
                            (int)rOrbitRocket + 150,
                            (int)rOrbitRocket + 1500
                        ),
                        r.NextDouble() * 2 * Math.PI
                    );

                model.meteors[i].phisObj.coord = coord;

                int dirOfRotat = r.NextDouble() < 0.5 ? 1 : -1;

                model.meteors[i].phisObj.speed = Gravitation.CosmicSpeeds(model.meteors[i].phisObj, model.centerPlanet.phisObj, dirOfRotat);
                model.meteors[i].phisObj.speed +=
                    Coord.FromPolar(
                            r.NextDouble() * model.meteors[i].phisObj.speed.polarR * 0.5,
                            r.NextDouble() * 2 * Math.PI
                        );
            }

            return model.meteors;
        }

        static public void SetActionsAndInteractions(GameModel model)
        {
            model.rocket.ActionAlwaysBeforeIntract += Rocket.JetTransEngineOperation;
            model.rocket.ActionAlwaysBeforeIntract += Rocket.JetRotatEngineOperation;

            model.rocket.ActionAlwaysAfterPhisic += Rocket.ResetValueOfEngine;

            model.rocket.phisObj.ActionAlways += RocketBody.JetTransEngineOperation;
            model.rocket.phisObj.ActionAlways += RocketBody.JetRotatEngineOperation;

            model.rocket.InteractToCondit.Add(new KeyValuePair<InteractCondition, Interaction>(Rocket.RocketIsCollided, Rocket.RocketCollide));


            model.fire.ActionAlwaysAfterIntract += ReactiveGases.UpdateIsVisio;

            foreach (GameObject go in model.gObjects)
            {
                go.InteractToCondit.Add(new KeyValuePair<InteractCondition, Interaction>(Collision.isCollided, Collision.ObjectsCollision));
            }

            model.CollisionTypes.Add(null, null, Collision.CollisionsOfAll);
            model.CollisionTypes.Add(typeof(Planet), null, Collision.CollisionWhithPlanet);
            model.CollisionTypes.Add(typeof(Rocket), null, Collision.CollisionWhithRocket);
            model.CollisionTypes.Add(typeof(Planet), typeof(Rocket), Collision.CollisionOfPlanetWhithRocket);
        }
    }
}
