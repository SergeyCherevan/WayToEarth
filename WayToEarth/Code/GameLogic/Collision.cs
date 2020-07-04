using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using WayToEarth.GameLogic;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.GameLogic
{
    static class Collision
    {
        public static bool isCollided(GameObject go1, GameObject go2, double timeInSec)
        {

            if (go1 is VisioObject || go2 is VisioObject ||
                !go1.isValid || !go2.isValid) return false;

            return Complex.Abs(go1.Coord - go2.Coord) < go1.Radius + go2.Radius;
        }

        public static void ObjectsCollision(GameObject go1, GameObject go2, double timeInSec)
        {
            if (go1.model.CollisionTypes.ContainsKey(go1.GetType(), go2.GetType()))
                go1.model.CollisionTypes[go1.GetType(), go2.GetType()](go1, go2, timeInSec);
            else
            if (go1.model.CollisionTypes.ContainsKey(go2.GetType(), go1.GetType()))
                go1.model.CollisionTypes[go2.GetType(), go1.GetType()](go2, go1, timeInSec);
            else
            if (go1.model.CollisionTypes.ContainsKey(go1.GetType(), null))
                go1.model.CollisionTypes[go1.GetType(), null](go1, go2, timeInSec);
            else
            if (go1.model.CollisionTypes.ContainsKey(go2.GetType(), null))
                go1.model.CollisionTypes[go2.GetType(), null](go2, go1, timeInSec);
            else
            if (go1.model.CollisionTypes.ContainsKey(null, null))
                go1.model.CollisionTypes[null, null](go2, go1, timeInSec);
        }

        public static void CollisionsOfAll(GameObject go1, GameObject go2, double timeInSec)
        {
            go1.isValid = false;
            go2.isValid = false;

            Bang bang = new Bang();
            go1.model.addedObjects.Add(bang);
            
            bang.image = MainWindow.Clone(MainWindow.window.Bang);            
            MainWindow.window.PlayingCanvas.Children.Add(bang.image);

            bang.X = (go1.X + go2.X) / 2;
            bang.Y = (go1.Y + go2.Y) / 2;
        }

        public static void CollisionWhithPlanet(GameObject planet, GameObject go, double timeInSec)
        {
            go.isValid = false;

            Bang bang = new Bang();
            planet.model.addedObjects.Add(bang);

            bang.image = MainWindow.Clone(MainWindow.window.BigBang);
            MainWindow.window.PlayingCanvas.Children.Add(bang.image);

            bang.X = go.X;
            bang.Y = go.Y;
        }

        public static void CollisionOfPlanetWhithRocket(GameObject planet, GameObject rocket, double timeInSec)
        {
            rocket.isValid = false;

            Bang bang = new Bang();
            planet.model.addedObjects.Add(bang);

            bang.image = MainWindow.Clone(MainWindow.window.LargeBang);
            MainWindow.window.PlayingCanvas.Children.Add(bang.image);

            bang.X = rocket.X;
            bang.Y = rocket.Y;
        }

        static Collision()
        {
            gConditInteractionNameMap.map.Add("isCollided", isCollided);

            InteractionNameMap.map.Add("ObjectsCollision", ObjectsCollision);
            InteractionNameMap.map.Add("CollisionsOfAll", CollisionsOfAll);
            InteractionNameMap.map.Add("CollisionWhithPlanet", CollisionWhithPlanet);
            InteractionNameMap.map.Add("CollisionOfPlanetWhithRocket", CollisionOfPlanetWhithRocket);
        }
    }
}
