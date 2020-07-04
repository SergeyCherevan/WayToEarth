using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using Newtonsoft.Json;
using static WayToEarth.GameLogic.GameObject;

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
            jsonSerializer.TypeNameHandling = TypeNameHandling.Auto;
            jsonSerializer.Formatting = Formatting.Indented;

            JsonWriter jsonWriter = new JsonTextWriter(file);
            jsonSerializer.Serialize(jsonWriter, gModel);

            jsonWriter.Close();
            file.Close();



            file = new StreamWriter("SevedGames.txt", true);

            file.WriteLine(DateString);

            file.Close();


            //Set value to lists planets, meteors and to object rocket.

            /*List<Planet> planets;

            SetValueToLocalListsOfGameObjects(out planets, out meteors);

            file.WriteLine("Level " + NumberOfLevel);

            file.WriteLine("Rocket:");
            WriteObjectParams(file, rocket);
            file.WriteLine("Translation " + rocket.translationEngine);
            file.WriteLine("Rotation " + rocket.rotationEngine);

            file.WriteLine("Planets:");
            WriteObjectParams(file, centerPlanet);
            foreach (Planet p in planets)
            {
                if (p != centerPlanet) WriteObjectParams(file, p);
            }


            file.WriteLine("Meteors:");
            foreach (Meteor m in meteors)
                WriteObjectParams(file, m);


            file.Close();





            file = new StreamWriter("SevedGames.txt", true);

            file.WriteLine(DateString);

            file.Close();*/
        }

        void SetValueToLocalListsOfGameObjects(out List<Planet> planets, out List<Meteor> meteors)
        {
            planets = new List<Planet>();
            meteors = new List<Meteor>();

            foreach (GameObject o in gModel.gObjects)
            {
                if (o is PhisicSimulatedGameObj)
                {
                    PhisicSimulatedGameObj go = o as PhisicSimulatedGameObj;

                    if (go is Planet) planets.Add(go as Planet);

                    if (go is Meteor) meteors.Add(go as Meteor);
                }
            }
        }




        static void WriteObjectParams(StreamWriter file, PhisicSimulatedGameObj go)
        {
            file.WriteLine("Coord " + go.X + " " + go.Y);

            file.WriteLine("Speed " + go.phisObj.Vx + " " + go.phisObj.Vy);

            file.WriteLine("Angle " + go.Angle);

            file.WriteLine("Angular Velosity " + go.phisObj.angulVel);

            file.WriteLine("Mass " + go.phisObj.mass);

            file.WriteLine("Momentum of inertia " + go.phisObj.inertMoment);
        }
    }
}