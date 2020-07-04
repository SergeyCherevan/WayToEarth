using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WayToEarth.GameLogic;

namespace WayToEarth
{
    static class ImageNameMap
    {
        public static Dictionary<string, Image> map;

        static ImageNameMap()
        {
            map = new Dictionary<string, Image>();

            AddImage(MainWindow.window.Border);
            AddImage(MainWindow.window.Planet);
            AddImage(MainWindow.window.SmallPlanet);
            AddImage(MainWindow.window.CenterStar);
            AddImage(MainWindow.window.Rocket);
            AddImage(MainWindow.window.Meteor);
            AddImage(MainWindow.window.Fire);
            AddImage(MainWindow.window.Bang);
            AddImage(MainWindow.window.BigBang);
            AddImage(MainWindow.window.LargeBang);
        }

        public static void AddImage(Image image)
        {
            map.Add(image.Name, image);
        }

        public static Image GetImage(string s)
        {
            if ( !map.ContainsKey(s) )
                throw new ApplicationException($"ImageNameMap has not got key \"{s}\"");

            List<string> duplicateImages = new List<string>() { "SmallPlanet", "Meteor", "Bang", "BigBang", "LargeBang", };

            if (duplicateImages.IndexOf(s) == -1) return map[s];

            Image image = MainWindow.Clone(map[s]);
            MainWindow.window.PlayingCanvas.Children.Add(image);

            return image;
        }

        public static string GetName(Image image) => map.First((e) => e.Value.Source == image.Source).Key;
    }
}
