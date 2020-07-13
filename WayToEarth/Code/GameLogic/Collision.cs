namespace WayToEarth.GameLogic
{
    static public class Collision
    {
        public static bool isCollided(GameObject go1, GameObject go2, double timeInSec)
        {

            if (go1 is VisioObject || go2 is VisioObject ||
                !go1.isValid || !go2.isValid) return false;

            return (go1.Coord - go2.Coord).polarR < go1.Radius + go2.Radius;
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
    }
}
