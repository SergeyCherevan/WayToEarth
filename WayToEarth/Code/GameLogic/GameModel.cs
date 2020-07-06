using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using WayToEarth.StaysOfWork;

namespace WayToEarth.GameLogic
{
    public class GameModel
    {
        public List<GameObject> gObjects { get; set; }

        public List<GameObject> addedObjects { get; set; }

        [JsonIgnore]
        public PhisicalModel phModel { get; set; }

        [JsonIgnore]
        public SetOfCollisionTypes CollisionTypes;

        public GameModel()
        {
            gObjects = new List<GameObject>();
            phModel = new PhisicalModel();
            addedObjects = new List<GameObject>();
            CollisionTypes = new SetOfCollisionTypes();
        }

        public void RegisterListOfGameObjects(List<GameObject> objects)
        {
            gObjects = objects;

            foreach (GameObject go in gObjects)
                go.model = this;

            phModel.RegisterListOfGameObjects(gObjects);
        }

        public GameModel Add(GameObject o)
        {
            gObjects.Add(o);
            if (o is PhisicSimulatedGameObj) phModel.Add(((PhisicSimulatedGameObj)o).phisObj);
            o.model = this;

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

                foreach (var cond in go1.ActToConditBeforeIntract)
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

                    if (go is PhisicSimulatedGameObj)
                        newPhModel.Add(   ((PhisicSimulatedGameObj)go).phisObj   );
                }
                else
                {
                    if (go is Meteor)
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

        static GameModel()
        {
            MethodNameMap<GameObject.Action>.AddMethod(Win);
            MethodNameMap<GameObject.Action>.AddMethod(Loose);
        }
    }
}
