using System.Windows;

namespace WayToEarth.StaysOfWork
{
    partial class GameOverStay : PlayingStay
    {
        PlayingStay parentStay;


        public GameOverStay()
        {
            
        }

        public void SetResultOfPlaying(PlayingStay ps)
        {
            parentStay = ps;

            gModel = ps.gModel;
            centerPlanet = ps.centerPlanet;
            rocket = ps.rocket;
            playingBorder = ps.playingBorder;

            fire = ps.fire;

            scale = ps.scale;

            controlerMessageTurn = ps.controlerMessageTurn;
            logicMessageTurn = ps.logicMessageTurn;
            visioMessageTurn = ps.visioMessageTurn;
        }

        public override void Set()
        {

            MainWindow.window.GameOverGrid.Visibility = Visibility.Visible;

            StartController();

            //StartLogic();

            StartVisio();
        }

        public override void Remove()
        {
            base.Remove();

            MainWindow.window.GameOverGrid.Visibility = Visibility.Hidden;
        }
    }
}
