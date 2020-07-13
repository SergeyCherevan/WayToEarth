using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WayToEarth.StaysOfWork
{
    public partial class PlayingStay
    {

        public override void StartController() { }

        public override void Controller()
        {
            MainWindow.window.PlayingCanvas.Focus();

            KeyValuePair<Message, object> mes;

            while ((mes = controlerMessageTurn.popByCodes(
                    new List<Message>() {
                        Message.KeyDown,
                        Message.ClickOfPause
                    }
                )).Key != Message.EmptyTurn)
                switch (mes.Key)
                {
                    case Message.KeyDown:
                        KeyboardControl(mes.Value as KeyEventArgs);
                        break;

                    case Message.ClickOfPause:
                        logicMessageTurn.push(Message.ClickOfPause, null);
                        break;
                }
        }

        void KeyboardControl(KeyEventArgs arg)
        {
            switch (arg.Key)
            {
                case Key.Left:
                case Key.A:
                    logicMessageTurn.push(  Message.LeftCommand, null );
                    break;

                case Key.Right:
                case Key.D:
                    logicMessageTurn.push( Message.RightCommand, null );
                    break;

                case Key.Up:
                case Key.W:
                    logicMessageTurn.push(  Message.UpCommand, null );
                    break;

                case Key.Down:
                case Key.S:
                    logicMessageTurn.push(  Message.DownCommand, null );
                    break;

                case Key.Space:
                    logicMessageTurn.push(  Message.SpaceCommand, null );
                    break;

                case Key.P:
                    logicMessageTurn.push(Message.ClickOfPause, null);
                    break;
            }
        }



        public static void PauseClick(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfPause, null);
        }
    }
}
