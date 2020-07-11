using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.StaysOfWork.Levels
{
    class SavedGame : PlayingStay
    {
        public override int NumberOfLevel { get; set; }

        public string StrTimeOfGameSaving;

        public override List<GameObject> SetValueOfGameObjects()
        {
            string fileName = StrTimeOfGameSaving.Replace(".", "").Replace(":", "") + ".json";

            StreamReader file = new StreamReader(fileName, false);

            var jsonSerializer = new JsonSerializer();
            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializer.TypeNameHandling = TypeNameHandling.All;
            jsonSerializer.Formatting = Formatting.Indented;

            JsonReader jsonReader = new JsonTextReader(file);

            gModel = jsonSerializer.Deserialize<GameModel>(jsonReader);

            jsonReader.Close();
            file.Close();


            centerPlanet = gModel.GetGameObjectByType<Planet>();
            rocket = gModel.GetGameObjectByType<Rocket>();

            fire = gModel.GetGameObjectByType<ReactiveGases>();
            fire.rocket = rocket;

            playingBorder = gModel.GetGameObjectByType<PlayingBorder>();
            playingBorder.rocket = rocket;
            playingBorder.planet = centerPlanet;

            /*foreach (var e in gModel.gObjects)
            {
                MainWindow.window.CurrentText.Text += e.Size + "\t";
            }*/


            return gModel.gObjects;

            /*timeInterval = 0.5;

            string fileName = StrTimeOfGameSaving.Replace(".", "").Replace(":", "") + ".txt";
            file = new StreamReader(fileName);

            List<GameObject> gameObjects = new List<GameObject>();



            centerPlanet = new Planet();
            gameObjects.Add(centerPlanet);

            if (NumberOfLevel == 1) centerPlanet.image = MainWindow.window.Planet;
            else centerPlanet.image = MainWindow.window.CenterStar;


            rocket = new Rocket();
            gameObjects.Add(rocket);

            rocket.image = MainWindow.window.Rocket;


            fire = new ReactiveGases(rocket);

            fire.image = MainWindow.window.Fire;

            gameObjects.Add(fire);


            playingBorder = new PlayingBorder(centerPlanet, rocket);

            playingBorder.image = MainWindow.window.Border;

            gameObjects.Add(playingBorder);


            meteors = new List<Meteor>();


            //Get number of level from line "Level <num>"
            NumberOfLevel = Int32.Parse(file.ReadLine().Split(' ')[1]);

            //Inicialization go
            PhisicSimulatedGameObj go = new PhisicSimulatedGameObj();

            string line = file.ReadLine();
            while (line != null)
            {
                switch (line)
                {
                    case "Rocket:":
                        ReadObjectParams(file, rocket);

                        rocket.translationEngine = (Rocket.TranslationEngine)Int32.Parse(file.ReadLine().Split(' ')[1]);
                        rocket.rotationEngine = (Rocket.RotationEngine)Int32.Parse(file.ReadLine().Split(' ')[1]);

                        gameObjects.Add(rocket);

                        line = file.ReadLine();

                        break;


                    case "Planets:":
                        Planet newPlanet = centerPlanet;

                        while ((line = ReadObjectParams(file, newPlanet)) == "")
                        {
                            if (newPlanet != centerPlanet)
                            {
                                gameObjects.Add(newPlanet);

                                newPlanet.image = MainWindow.Clone(MainWindow.window.SmallPlanet);

                                MainWindow.window.PlayingCanvas.Children.Add(newPlanet.image);
                            }
                            newPlanet = new Planet();
                        }


                        break;


                    case "Meteors:":
                        Meteor meteor = new Meteor();

                        for (int i = 0; (line = ReadObjectParams(file, meteor)) == ""; i++)
                        {
                            gameObjects.Add(meteor);
                            meteors.Add(meteor);

                            countOfMeteors = i + 1;
                            if (i != 0)
                            {
                                meteor.image = MainWindow.Clone(MainWindow.window.Meteor);
                                MainWindow.window.PlayingCanvas.Children.Add(meteor.image);
                            }
                            else
                                meteor.image = MainWindow.window.Meteor;
                            meteor = new Meteor();
                        }


                        break;

                }


            }

            file.Close();

            return gameObjects;*/
        }



        static string ReadObjectParams(StreamReader file, PhisicSimulatedGameObj go)
        {
            string[] strs;

            try
            {
                strs = file.ReadLine().Split(' ');
            }
            catch (NullReferenceException)
            {
                return null;
            }

            try
            {
                (go.X, go.Y) = (Double.Parse(strs[1]), Double.Parse(strs[2]));
            }
            catch (IndexOutOfRangeException)
            {
                return strs[0];
            }


            strs = file.ReadLine().Split(' ');

            (go.phisObj.Vx, go.phisObj.Vy) = (Double.Parse(strs[1]), Double.Parse(strs[2]));



            strs = file.ReadLine().Split(' ');

            go.Angle = Double.Parse(strs[1]);



            strs = file.ReadLine().Split(' ');

            go.phisObj.angulVel = Double.Parse(strs[2]);


            strs = file.ReadLine().Split(' ');

            go.phisObj.mass = Double.Parse(strs[1]);


            strs = file.ReadLine().Split(' ');

            go.phisObj.inertMoment = Double.Parse(strs[3]);

            return "";
        }

        public override void SetActionsAndInteractions(List<GameObject> gameObjects)
        {
            /*rocket.ActionAlwaysBeforeIntract += Rocket.JetTransEngineOperation;
            rocket.ActionAlwaysBeforeIntract += Rocket.JetRotatEngineOperation;

            rocket.ActionAlwaysAfterPhisic += Rocket.ResetValueOfEngine;

            rocket.phisObj.ActionAlways += RocketBody.JetTransEngineOperation;
            rocket.phisObj.ActionAlways += RocketBody.JetRotatEngineOperation;

            rocket.InteractToCondit.Add(new KeyValuePair<InteractCondition, Interaction>(Rocket.RocketIsCollided, Rocket.RocketCollide));


            fire.ActionAlwaysAfterIntract += ReactiveGases.UpdateIsVisio;

            foreach (GameObject go in gameObjects)
            {
                go.InteractToCondit.Add(new KeyValuePair<InteractCondition, Interaction>(Collision.isCollided, Collision.ObjectsCollision));
            }*/

            gModel.CollisionTypes.Add(null, null, Collision.CollisionsOfAll);
            gModel.CollisionTypes.Add(typeof(Planet), null, Collision.CollisionWhithPlanet);
            gModel.CollisionTypes.Add(typeof(Planet), typeof(Rocket), Collision.CollisionOfPlanetWhithRocket);
        }
    }
}
