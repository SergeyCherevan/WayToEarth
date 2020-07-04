using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using static WayToEarth.MainProgramWork;

namespace WayToEarth.StaysOfWork
{
    class LevelsMenuStay : StayOfWork
    {

        public LevelsMenuStay()
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

            MainWindow.window.LevelsMenuGrid.Visibility = Visibility.Visible;

            MainWindow.window.GoLevel1.Click += Level1Click;

            MainWindow.window.GoLevel2.Click += Level2Click;

            MainWindow.window.GoLevel3.Click += Level3Click;

            MainWindow.window.GoDownloadedGame.Click += ListOfSavedGamesClick;
        }

        public override void Remove()
        {
            MainWindow.window.LevelsMenuGrid.Visibility = Visibility.Hidden;
        }

        public override void Controller()
        {
            KeyValuePair<Message, object> mes =
                controlerMessageTurn.popByCode(Message.ClickOfLevel1);

            if (mes.Key == Message.ClickOfLevel1)
                logicMessageTurn.push(mes);

            mes = controlerMessageTurn.popByCode(Message.ClickOfLevel2);

            if (mes.Key == Message.ClickOfLevel2)
                logicMessageTurn.push(mes);

            mes = controlerMessageTurn.popByCode(Message.ClickOfLevel3);

            if (mes.Key == Message.ClickOfLevel3)
                logicMessageTurn.push(mes);

            mes = controlerMessageTurn.popByCode(Message.ClickOfListOfSavedGames);

            if (mes.Key == Message.ClickOfListOfSavedGames)
                logicMessageTurn.push(mes);
        }

        public override WayToSetNewStay Logic()
        {
            KeyValuePair<Message, object> mes =
                logicMessageTurn.popByCode(Message.ClickOfLevel1);

            if (mes.Key == Message.ClickOfLevel1)
                return MainProgramWork.WayToSetNewStay.Level1;

            mes = logicMessageTurn.popByCode(Message.ClickOfLevel2);

            if (mes.Key == Message.ClickOfLevel2)
                return MainProgramWork.WayToSetNewStay.Level2;

            mes = logicMessageTurn.popByCode(Message.ClickOfLevel3);

            if (mes.Key == Message.ClickOfLevel3)
                return MainProgramWork.WayToSetNewStay.Level3;

            mes = logicMessageTurn.popByCode(Message.ClickOfListOfSavedGames);

            if (mes.Key == Message.ClickOfListOfSavedGames)
                return MainProgramWork.WayToSetNewStay.ListOfSavedGames;

            return WayToSetNewStay.NotSet;
        }

        public override void Visio() { }


        public static void Level1Click(object sender, EventArgs ea)
        {
           MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfLevel1, null);
        }


        public static void Level2Click(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfLevel2, null);
        }


        public static void Level3Click(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfLevel3, null);
        }


        public static void ListOfSavedGamesClick(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfListOfSavedGames, null);
        }
    }
}
