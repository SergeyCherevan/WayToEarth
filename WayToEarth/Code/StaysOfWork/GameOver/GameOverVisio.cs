﻿using System;
using System.Collections.Generic;
using WayToEarth.GameLogic;

namespace WayToEarth.StaysOfWork
{
    public partial class GameOverStay : PlayingStay
    {
        
        public override void StartVisio()
        {
            SetGemeOverTitle();

            centralObject = new GameObject();

            centralObject.Coord = rocket.Coord;
        }

        void SetGemeOverTitle()
        {
            KeyValuePair<Message, object> mes;

            mes = MainProgramWork.mainMessageTurn.popByCode(Message.GameOver);

            if (mes.Key != Message.GameOver)
                throw new ApplicationException("Messages to Visio block of GameOver public class haven\'t got GemeOver message");

            PlayingResult result = (PlayingResult)mes.Value;

            if (result.isWin) MainWindow.window.GameOverTitle.Text = "You Win!";
            else MainWindow.window.GameOverTitle.Text = "Game Over";
        }
    }
}
