using System;
using System.Collections.Generic;
using WayToEarth.GameLogic;

namespace WayToEarth.StaysOfWork
{
    partial class GameOverStay : PlayingStay
    {
        
        public override void StartVisio()
        {
            SetGemeOverTitle();

            centralObject = new GameObject();

            centralObject.X = rocket.X;
            centralObject.Y = rocket.Y;
        }

        void SetGemeOverTitle()
        {
            KeyValuePair<Message, object> mes;

            mes = visioMessageTurn.popByCode(Message.GameOver);

            if (mes.Key != Message.GameOver)
                throw new ApplicationException("Messages to Visio block of GameOver class haven\'t got GemeOver message");

            PlayingResult result = (PlayingResult)mes.Value;

            if (result.isWin) MainWindow.window.GameOverTitle.Text = "You Win!";
            else MainWindow.window.GameOverTitle.Text = "Game Over";
        }
    }
}
