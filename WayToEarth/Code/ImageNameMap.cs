using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace WayToEarth
{
    class ImageNameMap
    {
        public static Dictionary<string, Image> map;

        public static void Start()
        {
            map = new Dictionary<string, Image>();
        }

        public static void AddImage(Image image)
        {
            map.Add(image.Name, image);
        }

        public static Image GetImage(string s)
        {
            if (!map.ContainsKey(s))
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
