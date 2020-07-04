using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            map.Add("Border", MainWindow.window.Border);
            map.Add("Planet", MainWindow.window.Planet);
            map.Add("SmallPlanet", MainWindow.window.SmallPlanet);
            map.Add("CenterStar", MainWindow.window.CenterStar);
            map.Add("Rocket", MainWindow.window.Rocket);
            map.Add("Meteor", MainWindow.window.Meteor);
            map.Add("Fire", MainWindow.window.Fire);
            map.Add("Bang", MainWindow.window.Bang);
            map.Add("BigBang", MainWindow.window.BigBang);
            map.Add("LargeBang", MainWindow.window.LargeBang);
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
