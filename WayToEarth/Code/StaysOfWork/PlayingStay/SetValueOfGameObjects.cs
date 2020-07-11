using System.Collections.Generic;
using WayToEarth.GameLogic;

namespace WayToEarth.StaysOfWork
{
    abstract partial class PlayingStay
    {
        abstract public List<GameObject> SetValueOfGameObjects();

        abstract public void SetActionsAndInteractions(List<GameObject> gameObjects);
    }
}
