using System;
using System.Collections.Generic;
using System.Windows;
using static WayToEarth.MainProgramWork;

namespace WayToEarth.StaysOfWork
{
    public class StartMenuStay : StayOfWork
    {

        public StartMenuStay()
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

            MainWindow.window.StartMenuGrid.Visibility = Visibility.Visible;

            MainWindow.window.LetsPlay.Click += LetsPlayClick;
        }

        public override void Remove()
        {
            MainWindow.window.StartMenuGrid.Visibility = Visibility.Hidden;
        }

        public override void Controller()
        {
            KeyValuePair<Message, object> mes =
                controlerMessageTurn.popByCode(Message.ClickOfLetsPlay);

            if (mes.Key == Message.ClickOfLetsPlay)
                logicMessageTurn.push(mes);
        }

        public override WayToSetNewStay Logic()
        {
            KeyValuePair<Message, object> mes =
                logicMessageTurn.popByCode(Message.ClickOfLetsPlay);

            if (mes.Key == Message.ClickOfLetsPlay)
                return WayToSetNewStay.LevelMenu;

            return WayToSetNewStay.NotSet;
        }

        public override void Visio() { }


        public static void LetsPlayClick(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfLetsPlay, null);
        }
    }
}
