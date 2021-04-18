using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WayToEarth.GameLogic;
using WayToEarth.NameMaps;
using WayToEarth.StaysOfWork;

namespace WayToEarth
{
    class Program
    {
        static public void Main(string[] args)
        {
            var thread = new Thread(new ThreadStart(staMain));

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            Console.WriteLine("Levels are saved");
        }

        static public void staMain()
        {
            try
            {
                StarterImageMap();

                MainProgramWork.SetValueOfStaticMethodsMaps();

                GameModel gm = GameModelCreator.Create(1);
                SaveLevel(1, gm);

                gm = GameModelCreator.Create(2);
                SaveLevel(2, gm);

                gm = GameModelCreator.Create(3);
                SaveLevel(3, gm);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void SaveLevel(int level, GameModel gModel)
        {
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

        public static void StarterImageMap()
        {
            ImageNameMap.map = new Dictionary<string, Image>();

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\ManyStarsCircle.png"));
            ImageNameMap.map.Add("Border", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\Planet.png"));
            ImageNameMap.map.Add("Planet", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\Planet.png"));
            ImageNameMap.map.Add("SmallPlanet", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\CenterStar.png"));
            ImageNameMap.map.Add("CenterStar", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\Rocket.png"));
            ImageNameMap.map.Add("Rocket", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\Meteor.png"));
            ImageNameMap.map.Add("Meteor", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\Fire.png"));
            ImageNameMap.map.Add("Fire", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\Bang.png"));
            ImageNameMap.map.Add("Bang", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\Bang.png"));
            ImageNameMap.map.Add("BigBang", image);

            image = new Image();
            image.Source = new BitmapImage(new Uri(@"C:\Users\chere\source\repos\SergeyCherevan\WayToEarth\WayToEarth\Bang.png"));
            ImageNameMap.map.Add("LargeBang", image);
        }
    }
}
