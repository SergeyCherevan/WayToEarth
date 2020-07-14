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


            model.RegisterListOfGameObjects(model.gObjects);


            return model;
        }
    }
}
