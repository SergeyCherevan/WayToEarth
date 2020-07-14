using System.Windows;

namespace WayToEarth.StaysOfWork
{
    public partial class GameOverStay : PlayingStay
    {
        PlayingStay parentStay;


        public GameOverStay()
        {
            
        }

        public void SetResultOfPlaying(PlayingStay ps)
        {
            parentStay = ps;

            gModel = ps.gModel;
            gModel.centerPlanet = ps.gModel.centerPlanet;
            gModel.rocket = ps.gModel.rocket;
            gModel.playingBorder = ps.gModel.playingBorder;

            gModel.fire = ps.gModel.fire;

            scale = ps.scale;

            controlerMessageTurn = new MessageTurn();
            logicMessageTurn = new MessageTurn();
            visioMessageTurn = new MessageTurn();
        }

        public override void Set()
        {

            MainWindow.window.GameOverGrid.Visibility = Visibility.Visible;

            StartController();

            StartLogic();

            StartVisio();
        }

        public override void Remove()
        {
            base.Remove();

            MainWindow.window.GameOverGrid.Visibility = Visibility.Hidden;
        }
    }
}
