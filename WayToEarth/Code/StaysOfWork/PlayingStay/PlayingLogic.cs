using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;
using static WayToEarth.MainProgramWork;
//using WayToEarth.StaysOfWork;

namespace WayToEarth.StaysOfWork
{
    abstract partial class PlayingStay
    {
        public abstract int NumberOfLevel { get; set; }

        public double timeInterval = 1;

        public Planet centerPlanet;

        public Rocket rocket;

        public PlayingBorder playingBorder;

        public List<Meteor> meteors;
        public int countOfMeteors = 150;

        public GameModel gModel;

        override public void StartLogic()
        {
            gModel = new GameModel();

            List<GameObject> gameObjects = SetValueOfGameObjects();

            gModel.RegisterListOfGameObjects(gameObjects);

            gModel.phModel.SetGravitationInteractive();


            SetActionsAndInteractions(gameObjects);
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

                        visioMessageTurn.push(mes);

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
