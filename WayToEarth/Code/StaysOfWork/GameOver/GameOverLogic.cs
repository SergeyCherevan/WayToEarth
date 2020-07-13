using System.Collections.Generic;
using WayToEarth.GameLogic;
using static WayToEarth.MainProgramWork;

namespace WayToEarth.StaysOfWork
{
    public partial class GameOverStay : PlayingStay
    {
        public override void StartLogic() { }

        public override WayToSetNewStay Logic()
        {
            base.Logic();

            KeyValuePair<Message, object> mes;

            mes = logicMessageTurn.popByCode(Message.ClickOfBackToMenu);

            if (mes.Key == Message.ClickOfBackToMenu)
                    return WayToSetNewStay.StartMenu;


            mes = logicMessageTurn.popByCode(Message.ClickOfPlayAgain);

            if (mes.Key == Message.ClickOfPlayAgain)
                return MainProgramWork.setOfStay.KeyOf(parentStay);

            return WayToSetNewStay.NotSet;
        }

        override public GameModel SetValueOfGameModel(string str) { return null; }

        public List<GameObject> SetValueOfMainGameObjects() { return null; }

        public void SetValueOfMeteors(List<GameObject> gameObjects) { }

        public List<GameObject> SetValueOfSecondaryGameObjects() { return null; }

        override public void SaveGame() { }
    }
}
