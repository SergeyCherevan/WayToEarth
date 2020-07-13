using System.Collections.Generic;
using WayToEarth.GameLogic;
using static WayToEarth.MainProgramWork;

namespace WayToEarth.StaysOfWork
{
    public partial class PlayingStay
    {
        public double timeInterval = 1;

        public Planet centerPlanet;

        public Rocket rocket;

        public PlayingBorder playingBorder;

        public List<Meteor> meteors;
        public List<Planet> planets;
        public int countOfMeteors = 150;

        public GameModel gModel;

        string fileName = "";

        override public void StartLogic()
        {
            var mes = mainMessageTurn.popByCodes(new List<Message>()
                {
                    Message.ClickOfLevel1,
                    Message.ClickOfLevel2,
                    Message.ClickOfLevel3,
                });

            if (mes.Key != Message.EmptyTurn)
            {
                fileName = $"Level {mes.Key - Message.ClickOfLevel1 + 1}.json";
            }

            mes = mainMessageTurn.popByCode(Message.ClickOfSavedGame);

            if (mes.Key != Message.EmptyTurn)
            {
                fileName = mes.Value as string;
            }

            gModel = SetValueOfGameModel(fileName);
        }

        public override WayToSetNewStay Logic()
        {
            WayToSetNewStay hcm = HandlingControlMessages();

            ComputingOfModeling();

            WayToSetNewStay hgm = HandlingGameMessages();

            return hcm != WayToSetNewStay.NotSet ? hcm : hgm;
        }


        WayToSetNewStay HandlingControlMessages()
        {
            bool isPause = false;

            KeyValuePair<Message, object> mes;

            while ((mes = logicMessageTurn.popByCodes(
                    new List<Message>() {
                         Message.LeftCommand,
                         Message.RightCommand,
                         Message.UpCommand,
                         Message.DownCommand,
                         Message.SpaceCommand,
                         Message.ClickOfPause,
                    }
                )).Key != Message.EmptyTurn)
                switch (mes.Key)
                {
                    case Message.UpCommand:
                        rocket.translationEngine = Rocket.TranslationEngine.Front;
                        break;

                    case Message.DownCommand:
                        rocket.translationEngine = Rocket.TranslationEngine.Back;
                        break;

                    case Message.LeftCommand:
                        rocket.rotationEngine = Rocket.RotationEngine.Left;
                        break;

                    case Message.RightCommand:
                        rocket.rotationEngine = Rocket.RotationEngine.Right;
                        break;

                    case Message.ClickOfPause:
                        isPause = true;
                        break;
                }

            if (isPause) return WayToSetNewStay.Pause;

            return WayToSetNewStay.NotSet;
        }



        WayToSetNewStay HandlingGameMessages()
        {
            KeyValuePair<Message, object> mes;

            while ((mes = logicMessageTurn.popByCode(Message.GameOver)).Key != Message.EmptyTurn)
                switch (mes.Key)
                {
                    case Message.GameOver:

                        mainMessageTurn.push(mes);

                        WayToSetNewStay newStay = WayToSetNewStay.GameOver;
                        return newStay;                        
                }

            return WayToSetNewStay.NotSet;
        }



        void ComputingOfModeling()
        {
            gModel.DeleteInvalidObjects();



            gModel.ComputingOfActionsBeforeIntract(timeInterval);

            gModel.ComputingOfInteractions(timeInterval);

            gModel.ComputingOfActionsAfterIntract(timeInterval);



            gModel.phModel.ComputingOfActions(timeInterval);

            gModel.phModel.ComputingOfInteractions(timeInterval);

            gModel.phModel.ComputingOfMoving(timeInterval);



            gModel.ComputingOfActionsAfterPhisic(timeInterval);



            gModel.AddNewObjects();
        }


    }
}
