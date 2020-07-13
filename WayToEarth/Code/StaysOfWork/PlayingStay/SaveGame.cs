using System;
using System.IO;
using Newtonsoft.Json;

namespace WayToEarth.StaysOfWork
{
    public partial class PlayingStay
    {
        virtual public void SaveGame()
        {
            string DateString = DateTime.Now.ToString();

            StreamWriter file = new StreamWriter(DateString.Replace(".", "").Replace(":", "") + ".json", false);

            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializer.TypeNameHandling = TypeNameHandling.Auto;
            jsonSerializer.Formatting = Formatting.Indented;

            JsonWriter jsonWriter = new JsonTextWriter(file);
            jsonSerializer.Serialize(jsonWriter, gModel);

            jsonWriter.Close();
            file.Close();



            file = new StreamWriter("SevedGames.txt", true);

            file.WriteLine(DateString);

            file.Close();

        }
    }
}