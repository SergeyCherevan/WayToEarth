using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using static WayToEarth.MainProgramWork;

namespace WayToEarth.StaysOfWork
{
    class ListOfSavedGamesStay : StayOfWork
    {

        public ListOfSavedGamesStay()
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

            MainWindow.window.ListOfSavedGamesStack.Visibility = Visibility.Visible;

            MainWindow.window.GoToMenuFromSavedGame.Click += PauseStay.BackToMenuClick;

            foreach (UIElement e in MainWindow.window.ListOfSavedGamesStack.Children)
                if (e != MainWindow.window.GoToMenuFromSavedGame)
                    e.Visibility = Visibility.Collapsed;

            StreamReader file = new StreamReader("SevedGames.txt", true);

            string line = file.ReadLine();

            while(line != null)
            {
                Button button = MainWindow.Clone(MainWindow.window.GoToMenuFromSavedGame);
                button.Visibility = Visibility.Visible;
                button.Click += GoSavedGamesClick;
                button.Content = line;
                MainWindow.window.ListOfSavedGamesStack.Children.Add(button);

                line = file.ReadLine();
            }

            file.Close();
        }

        public override void Remove()
        {
            MainWindow.window.ListOfSavedGamesStack.Visibility = Visibility.Hidden;
        }

        public override void Controller()
        {
            KeyValuePair<Message, object> mes =
                controlerMessageTurn.popByCode(Message.ClickOfSavedGame);

            if (mes.Key != Message.EmptyTurn)
                logicMessageTurn.push(mes);

            mes = controlerMessageTurn.popByCode(Message.ClickOfBackToMenu);

            if (mes.Key != Message.EmptyTurn)
                logicMessageTurn.push(mes);
        }

        public override WayToSetNewStay Logic()
        {
            KeyValuePair<Message, object> mes =
                logicMessageTurn.popByCode(Message.ClickOfSavedGame);

            if (mes.Key != Message.EmptyTurn)
            {
                string fileName = (mes.Value as Button).Content as string;
                fileName = fileName.Replace(".", "").Replace(":", "") + ".json";

                mainMessageTurn.push(
                        Message.ClickOfSavedGame,
                        fileName
                    ); 

                return WayToSetNewStay.SavedGame;
            }


            mes = logicMessageTurn.popByCode(Message.ClickOfBackToMenu);

            if (mes.Key != Message.EmptyTurn)
            {                
                return WayToSetNewStay.StartMenu;
            }


            return WayToSetNewStay.NotSet;
        }

        public override void Visio() { }


        public static void GoSavedGamesClick(object sender, EventArgs ea)
        {
           MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfSavedGame, sender);
        }
    }
}
