using System.Collections.Generic;
using WayToEarth.GameLogic;

namespace WayToEarth.Phisic
{
    public class PhisicalModel
    {
        public List<PhisicalObject> pObjects { get; set; }

        public PhisicalModel()
        {
            pObjects = new List<PhisicalObject>();
        }

        public void RegisterListOfGameObjects(List<GameObject> gObjects)
        {
            foreach (GameObject go in gObjects)
            {
                if (go is PhisicSimulatedGameObj)
                    pObjects.Add( ((PhisicSimulatedGameObj)go).phisObj );
            }
        }

        public PhisicalModel Add(PhisicalObject o)
        {
            pObjects.Add(o);

            return this;
        }

        public void SetGravitationInteractive()
        {
            foreach (PhisicalObject po in pObjects)
            {
                po.InteractionWithAll += Gravitation.GravitationalInteraction;
            }
        }

        public void ComputingOfInteractions(double timeInSec)
        {

            foreach (PhisicalObject po1 in pObjects)
            {
                foreach (PhisicalObject po2 in pObjects)
                {
                    if(po1 != po2)po1.InteractionWithAll?.Invoke(po1, po2, timeInSec);

                    foreach (var cond in po1.InteractToCondit)
                    {
                        if (cond.Key(po1, po2, timeInSec)) cond.Value(po1, po2, timeInSec);
                    }
                }
            }
        }

        public void ComputingOfActions(double timeInSec)
        {

            foreach (PhisicalObject po1 in pObjects)
            {
                po1.ActionAlways?.Invoke(po1, timeInSec);

                foreach (var cond in po1.ActToCondit)
                {
                    if (cond.Key(po1, timeInSec)) cond.Value(po1, timeInSec);
                }
            }
        }

        public void ComputingOfMoving(double timeInSecond)
        {
            foreach (PhisicalObject po in pObjects)
            {
                po.Move(timeInSecond);
            }
        }
    }
}
