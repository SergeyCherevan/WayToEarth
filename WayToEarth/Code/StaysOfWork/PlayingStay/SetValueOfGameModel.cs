using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WayToEarth.GameLogic;

namespace WayToEarth.StaysOfWork
{
    public partial class PlayingStay
    {
        public virtual GameModel SetValueOfGameModel(string fileName)
        {
            StreamReader file = new StreamReader(fileName, false);

            var jsonSerializer = new JsonSerializer();
            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializer.TypeNameHandling = TypeNameHandling.Auto;
            jsonSerializer.Formatting = Formatting.Indented;

            JsonReader jsonReader = new JsonTextReader(file);

            GameModel model = jsonSerializer.Deserialize<GameModel>(jsonReader);

            jsonReader.Close();
            file.Close();


            centerPlanet = model.GetGameObjectByType<Planet>();
            rocket = model.GetGameObjectByType<Rocket>();

            fire = model.GetGameObjectByType<ReactiveGases>();
            fire.rocket = rocket;

            playingBorder = model.GetGameObjectByType<PlayingBorder>();
            playingBorder.rocket = rocket;
            playingBorder.planet = centerPlanet;

            meteors = new List<Meteor>();
            planets = new List<Planet>();
            foreach (GameObject go in model.gObjects)
            {
                if (go is Meteor m) meteors.Add(m);
                if (go is Planet p) planets.Add(p);
            }


            model.RegisterListOfGameObjects(model.gObjects);

            model.phModel.SetGravitationInteractive();


            return model;
        }
    }
}
