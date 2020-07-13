﻿using System;
using System.Collections.Generic;

namespace WayToEarth.StaysOfWork
{
    public partial class GameOverStay : PlayingStay
    {
        public override void StartController()
        {
            MainWindow.window.BackToMenu.Click += PauseStay.BackToMenuClick;
            MainWindow.window.PlayAgain.Click += PlayAgainClick;
        }

        public override void Controller()
        {
            KeyValuePair<Message, object> mes;

            mes = controlerMessageTurn.popByCode(Message.ClickOfBackToMenu);

            if (mes.Key == Message.ClickOfBackToMenu)
                logicMessageTurn.push(mes);


            mes = controlerMessageTurn.popByCode(Message.ClickOfPlayAgain);

            if (mes.Key == Message.ClickOfPlayAgain)
                logicMessageTurn.push(mes);
        }

        public static void PlayAgainClick(object sender, EventArgs ea)
        {
            MainProgramWork.currently.controlerMessageTurn.push(Message.ClickOfPlayAgain, null);
        }
    }
}
