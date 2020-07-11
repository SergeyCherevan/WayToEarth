using WayToEarth.GameLogic;
using WayToEarth.Phisic;

namespace WayToEarth
{
    static class StarterNameMap
    {
        static public void Start(MethodNameMap<GameObject.Action> q)
        {
            MethodNameMap<GameObject.Action>.Start();

            MethodNameMap<GameObject.Action>.AddMethod(Bang.Burning);

            MethodNameMap<GameObject.Action>.AddMethod(GameModel.Win);
            MethodNameMap<GameObject.Action>.AddMethod(GameModel.Loose);

            MethodNameMap<GameObject.Action>.AddMethod(ReactiveGases.UpdateIsVisio);

            MethodNameMap<GameObject.Action>.AddMethod(Rocket.JetRotatEngineOperation);
            MethodNameMap<GameObject.Action>.AddMethod(Rocket.JetTransEngineOperation);
            MethodNameMap<GameObject.Action>.AddMethod(Rocket.ResetValueOfEngine);
        }


        static public void Start(MethodNameMap<GameObject.Interaction> q)
        {
            MethodNameMap<GameObject.Interaction>.Start();

            MethodNameMap<GameObject.Interaction>.AddMethod(Collision.ObjectsCollision);
            MethodNameMap<GameObject.Interaction>.AddMethod(Collision.CollisionsOfAll);
            MethodNameMap<GameObject.Interaction>.AddMethod(Collision.CollisionWhithPlanet);
            MethodNameMap<GameObject.Interaction>.AddMethod(Collision.CollisionOfPlanetWhithRocket);

            MethodNameMap<GameObject.Interaction>.AddMethod(Rocket.RocketCollide);
        }

        static public void Start(MethodNameMap<GameObject.ActCondition> q)
        {
            MethodNameMap<GameObject.ActCondition>.Start();

            MethodNameMap<GameObject.ActCondition>.AddMethod(PlayingBorder.WentAbroad);
        }

        static public void Start(MethodNameMap<GameObject.InteractCondition> q)
        {
            MethodNameMap<GameObject.InteractCondition>.Start();

            MethodNameMap<GameObject.InteractCondition>.AddMethod(Collision.isCollided);

            MethodNameMap<GameObject.InteractCondition>.AddMethod(Rocket.RocketIsCollided);
        }

        static public void Start(MethodNameMap<PhisicalObject.Interaction> q)
        {
            MethodNameMap<PhisicalObject.Interaction>.Start();

            MethodNameMap<PhisicalObject.Interaction>.AddMethod(Gravitation.GravitationalInteraction);
        }

        static public void Start(MethodNameMap<PhisicalObject.Action> q)
        {
            MethodNameMap<PhisicalObject.Action>.Start();

            MethodNameMap<PhisicalObject.Action>.AddMethod(RocketBody.JetRotatEngineOperation);
            MethodNameMap<PhisicalObject.Action>.AddMethod(RocketBody.JetTransEngineOperation);
        }

        static public void Start(MethodNameMap<PhisicalObject.ActCondition> q)
        {
            MethodNameMap<PhisicalObject.ActCondition>.Start();
        }

        static public void Start(MethodNameMap<PhisicalObject.InteractCondition> q)
        {
            MethodNameMap<PhisicalObject.InteractCondition>.Start();
        }

        static public void Start(ImageNameMap q)
        {
            ImageNameMap.Start();

            ImageNameMap.AddImage(MainWindow.window.Border);
            ImageNameMap.AddImage(MainWindow.window.Planet);
            ImageNameMap.AddImage(MainWindow.window.SmallPlanet);
            ImageNameMap.AddImage(MainWindow.window.CenterStar);
            ImageNameMap.AddImage(MainWindow.window.Rocket);
            ImageNameMap.AddImage(MainWindow.window.Meteor);
            ImageNameMap.AddImage(MainWindow.window.Fire);
            ImageNameMap.AddImage(MainWindow.window.Bang);
            ImageNameMap.AddImage(MainWindow.window.BigBang);
            ImageNameMap.AddImage(MainWindow.window.LargeBang);
        }
    }
}
