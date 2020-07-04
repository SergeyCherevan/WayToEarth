using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.StaysOfWork
{
    abstract partial class PlayingStay
    {
        abstract public List<GameObject> SetValueOfGameObjects();

        abstract public void SetActionsAndInteractions(List<GameObject> gameObjects);
    }
}
