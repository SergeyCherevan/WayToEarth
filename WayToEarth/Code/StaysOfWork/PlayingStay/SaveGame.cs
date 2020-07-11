using System;
using System.Collections.Generic;
using System.IO;
using WayToEarth.GameLogic;
using Newtonsoft.Json;

namespace WayToEarth.StaysOfWork
{
    abstract partial class PlayingStay
    {
        virtual public void SaveGame()
        {
            string DateString = DateTime.Now.ToString();

            StreamWriter file = new StreamWriter(DateString.Replace(".", "").Replace(":", "") + ".json", false);

            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializer.TypeNameHandling = TypeNameHandling.All;
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