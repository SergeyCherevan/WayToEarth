using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WayToEarth.NameMaps;
using WayToEarth.Phisic;
using WayToEarth.StaysOfWork;

namespace WayToEarth.GameLogic
{
    public class GameModel
    {
        public List<GameObject> gObjects { get; set; }

        public List<GameObject> addedObjects { get; set; }

        /**/

        [JsonIgnore] public Planet centerPlanet;

        [JsonIgnore] public Rocket rocket;

        [JsonIgnore] public PlayingBorder playingBorder;

        [JsonIgnore] public List<Meteor> meteors;
        [JsonIgnore] public List<Planet> planets;


        [JsonIgnore] public ReactiveGases fire;

        /**/

        [JsonIgnore] public PhisicalModel phModel;

        [JsonIgnore]
        public SetOfCollisionTypes CollisionTypes;


        public MessageTurn messageTurn;


        public string strCollisionTypes
        {
            get => CollisionTypes.TypesPairsToNamesP();

            set { CollisionTypes = value.NamesPairsToTypesP(); }
        }

        public GameModel()
        {
            gObjects = new List<GameObject>();
            phModel = new PhisicalModel();
            addedObjects = new List<GameObject>();
            CollisionTypes = new SetOfCollisionTypes();

            meteors = new List<Meteor>();
            planets = new List<Planet>();

            messageTurn = new MessageTurn();
        }

        public void RegisterListOfGameObjects(List<GameObject> objects)
        {
            gObjects = objects;

            foreach (GameObject go in gObjects)
            {
                go.model = this;

                if (go is Meteor m)
                    meteors.Add(m);

                if (go is Planet p)
                    planets.Add(p);
            }

            centerPlanet = GetGameObjectByType<Planet>();
            rocket = GetGameObjectByType<Rocket>();

            fire = GetGameObjectByType<ReactiveGases>();
            fire.rocket = rocket;

            playingBorder = GetGameObjectByType<PlayingBorder>();
            playingBorder.rocket = rocket;
            playingBorder.planet = centerPlanet;

            phModel.RegisterListOfGameObjects(gObjects);
        }

        public GameModel Add(GameObject go)
        {
            gObjects.Add(go);

            if (go is PhisicSimulatedGameObj)
                phModel.Add(((PhisicSimulatedGameObj)go).phisObj);

            go.model = this;

            if (go is Meteor m)
                meteors.Add(m);

            if (go is Planet p)
                planets.Add(p);

            return this;
        }

        public void ComputingOfInteractions(double timeInSec)
        {

            foreach (GameObject go1 in gObjects)
            {
                foreach (GameObject go2 in gObjects)
                {
                    if(go1 != go2)go1.InteractionWithAll?.Invoke(go1, go2, timeInSec);

                    foreach (var cond in go1.InteractToCondit)
                    {
                        if (go1 != go2 && cond.Key(go1, go2, timeInSec)) cond.Value(go1, go2, timeInSec);
                    }
                }
            }
        }

        public void ComputingOfActionsBeforeIntract(double timeInSec)
        {

            foreach (GameObject go1 in gObjects)
            {
                go1.ActionAlwaysBeforeIntract?.Invoke(go1, timeInSec);

                foreach (var cond in go1.ActToConditBeforeInteract)
                {
                    if (cond.Key(go1, timeInSec)) cond.Value(go1, timeInSec);
                }
            }
        }

        public void ComputingOfActionsAfterIntract(double timeInSec)
        {
            foreach (GameObject go1 in gObjects)
            {
                go1.ActionAlwaysAfterIntract?.Invoke(go1, timeInSec);

                foreach (var cond in go1.ActToConditAfterIntract)
                {
                    if (cond.Key(go1, timeInSec)) cond.Value(go1, timeInSec);
                }
            }
        }

        public void ComputingOfActionsAfterPhisic(double timeInSec)
        {

            foreach (GameObject go1 in gObjects)
            {
                go1.ActionAlwaysAfterPhisic?.Invoke(go1, timeInSec);

                foreach (var cond in go1.ActToConditAfterPhisic)
                {
                    if (cond.Key(go1, timeInSec)) cond.Value(go1, timeInSec);
                }
            }
        }

        public void DeleteInvalidObjects()
        {
            MainWindow.iterat++;

            List<GameObject> newgObjects = new List<GameObject>();
            PhisicalModel newPhModel = new PhisicalModel();

            foreach (GameObject go in gObjects)
            {
                if (go.isValid)
                {
                    newgObjects.Add(go);

                    if (go is PhisicSimulatedGameObj pgo)
                        newPhModel.Add(pgo.phisObj);
                }
                else
                {
                    if (go.ImageName == "SmallPlanet")
                        MainWindow.window.PlayingCanvas.Children.Remove(go.image);

                    if (go.ImageName == "Meteor")
                        MainWindow.window.PlayingCanvas.Children.Remove(go.image);

                    if (go.ImageName == "Bang")
                        MainWindow.window.PlayingCanvas.Children.Remove(go.image);

                    if (go.ImageName == "BigBang")
                        MainWindow.window.PlayingCanvas.Children.Remove(go.image);

                    if (go.ImageName == "LargeBang")
                        MainWindow.window.PlayingCanvas.Children.Remove(go.image);
                }
            }

            gObjects = newgObjects;
            phModel = newPhModel;
        }

        public void AddNewObjects()
        {
            gObjects = ( gObjects.Concat(addedObjects) ).ToList();
            addedObjects = new List<GameObject>();
        }

        public TamplateOfGameObject GetGameObjectByType<TamplateOfGameObject>()
            where TamplateOfGameObject : GameObject
        {
            return gObjects.Find(
                    delegate (GameObject o)
                    {
                        return o.GetType() == typeof(TamplateOfGameObject);
                    }
                ) as TamplateOfGameObject;
        }

        public static void Win(GameObject go, double timeInSec)
        {
            PlayingResult result = new PlayingResult(true, 3);

            MainProgramWork.currently.logicMessageTurn.push(Message.GameOver, result);
        }

        public static void Loose(GameObject go, double timeInSec)
        {
            PlayingResult result = new PlayingResult(false, 0);

            MainProgramWork.currently.logicMessageTurn.push(Message.GameOver, result);
        }
    }
}
