using System.Windows;

namespace WayToEarth.StaysOfWork
{
    abstract partial class PlayingStay : StayOfWork
    {
        
        public PlayingStay()
        {
            
        }



        public override void Set()
        {
            controlerMessageTurn = new MessageTurn();
            logicMessageTurn = new MessageTurn();
            visioMessageTurn = new MessageTurn();

            MainWindow.window.PlayingCanvas.Visibility = Visibility.Visible;

            MainWindow.window.Pause.Click += PauseClick;

            StartController();

            StartLogic();

            StartVisio();
        }

        public override void Remove()
        {
            VisioRemove();

            MainWindow.window.PlayingCanvas.Visibility = Visibility.Hidden;
        }
    }
}
