using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using static WayToEarth.MainProgramWork;

namespace WayToEarth.StaysOfWork
{
    class PauseStay : StayOfWork
    {
        public PlayingStay currentGameStay;

        public PauseStay()
        {

        }


        public override void StartController() { }
        public override void StartLogic() { }
        public override void StartVisio() { }


        public override void Set()
        {
            controlerMessageTurn = new MessageTurn();
            logicMessageTurn = new MessageTurn();
            visioMessageTurn = new MessageTurn();

            MainWindow.window.PauseGrid.Visibility = Visibility.Visible;

            MainWindow.window.PlayNext.Click += PlayNextClick;
            MainWindow.window.BackToMenuPause.Click += BackToMenuClick;
            MainWindow.window.Save.Click += SaveClick;
        }

        public void SetResultOfPlaying(PlayingStay ps)
        {
            currentGameStay = ps;
        }

        public override void Remove()
        {
            currentGameStay.Remove();

            MainWindow.window.PauseGrid.Visibility = Visibility.Hidden;
        }

        public void RemoveToPlay()
        {
            MainWindow.window.PauseGrid.Visibility = Visibility.Hidden;
        }

        public override void Controller()
        {
            KeyValuePair<Message, object> mes;

            while ((mes = controlerMessageTurn.popByCodes(
                    new List<Message>() {
                        Message.ClickOfPlayNext,
                        Message.ClickOfBackToMenu,
                        Message.ClickOfSave,
                        Message.KeyDown,
                    }
                )).Key != Message.EmptyTurn)
                switch (mes.Key)
                {
                    case Message.KeyDown:
                        if ( (mes.Value as KeyEventArgs).Key == Key.P )
                            logicMessageTurn.push(Message.ClickOfPlayNext, null);
                        break;

                    case Message.ClickOfPlayNext:
                        logicMessageTurn.push(mes);
                        break;

                    case Message.ClickOfBackToMenu:
                        logicMessageTurn.push(mes);
                        break;

                    case Message.ClickOfSave:
                        logicMessageTurn.push(mes);
                        break;
                }
        }

        public override WayToSetNewStay Logic()
        {
            bool isPlayNext = false, isSaving = false, isBackToMenu = false;

            KeyValuePair<Message, object> mes;

            while ((mes = logicMessageTurn.popByCodes(
                    new List<Message>() {
                        Message.ClickOfPlayNext,
                        Message.ClickOfBackToMenu,
                        Message.ClickOfSave,
                        Message.ClickOfBackToMenu,
                    }
                )).Key != Message.EmptyTurn)
                switch (mes.Key)
                {
                    case Message.ClickOfPlayNext:
                        isPlayNext = true;
                        break;

                    case Message.ClickOfBackToMenu:
                        isBackToMenu = true;
                        break;

                    case Message.ClickOfSave:
                        isSaving = true;
                        break;
                }

            if (isPlayNext)            
                return WayToSetNewStay.FromPauseToPlay;

            if (isBackToMenu)
                return WayToSetNewStay.StartMenu;

            if (isSaving)
                currentGameStay.SaveGame();

            return WayToSetNewStay.NotSet;
        }

        public override void Visio() { }


        public static void PlayNextClick(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfPlayNext, null);
        }


        public static void BackToMenuClick(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfBackToMenu, null);
        }


        public static void SaveClick(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfSave, null);
        }
    }
}
