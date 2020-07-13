using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.Json;
using WayToEarth.GameLogic;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JsonSaving
{
    public class Program
    {

        static GameModel model;

        static void Main(string[] args)
        {

            while(true)
            {

                string input = Console.ReadLine();

                Console.WriteLine("");

                switch (input)
                {
                    case "wc":
                        {
                            model = new GameModel();

                            GameObject o = new Meteor();
                            model.Add(o);

                            (o.X, o.Y) = (100, 200);

                            string json = JsonConvert.SerializeObject(model, Formatting.Indented);

                            StreamWriter file = new StreamWriter("JSON.json", false);

                            Console.WriteLine(json);
                            file.WriteLine(json);

                            file.Close();
                        }

                        break;

                    case "ws":
                        {
                            model = new GameModel();

                            GameObject o = new Meteor();
                            model.Add(o);

                            (o.X, o.Y) = (100, 200);

                            string json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true, });

                            StreamWriter file = new StreamWriter("JSON.json", false);

                            Console.WriteLine(json);
                            file.WriteLine(json);

                            file.Close();
                        }

                        break;


                    case "wsj":
                        {
                            model = new GameModel();

                            GameObject o = new Meteor();
                            model.Add(o);

                            (o.X, o.Y) = (100, 200);

                            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                            jsonSerializer.TypeNameHandling = TypeNameHandling.Auto;
                            jsonSerializer.Formatting = Formatting.Indented;

                            StreamWriter wfile = new StreamWriter("JSON.json", false);
                            JsonWriter jsonWriter = new JsonTextWriter(wfile);

                            jsonSerializer.Serialize(jsonWriter, model);

                            jsonWriter.Close();
                            wfile.Close();



                            StreamReader rfile = new StreamReader("JSON.json");
                            string json = rfile.ReadToEnd();
                            rfile.Close();

                            Console.WriteLine(json);
                        }

                        break;

                    case "r":

                        {
                            StreamReader file = new StreamReader("JSON.json");

                            string json = file.ReadToEnd();

                            GameModel model = JsonSerializer.Deserialize<GameModel>(json);

                            GameObject o = model.gObjects[0];

                            json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true, });

                            Console.WriteLine(json);
                        }

                        break;


                    case "rsj":
                        {
                            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                            jsonSerializer.TypeNameHandling = TypeNameHandling.Auto;
                            jsonSerializer.Formatting = Formatting.Indented;

                            StreamReader file = new StreamReader("JSON.json");
                            JsonReader jsonReader = new JsonTextReader(file);

                            model = jsonSerializer.Deserialize<GameModel>(jsonReader);

                            jsonReader.Close();
                            file.Close();



                            GameObject o = model.gObjects[0];

                            string json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true, });

                            Console.WriteLine(json);
                        }

                        break;
                }

                Console.Write("\n\n");

            }

        }

        static void SetModelValue()
        {

        }
    }
}
