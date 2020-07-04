using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Reflection;
using WayToEarth.StaysOfWork;
using WayToEarth.StaysOfWork.Levels;
using System.Linq;
using WayToEarth.GameLogic;

namespace WayToEarth
{
    static class MainProgramWork
    {
        public enum WayToSetNewStay
        {
            NotSet = -1,

            StartMenu,
            LevelMenu,
            Level1,
            Level2,
            Level3,
            ListOfSavedGames,
            SavedGame,
            Pause,
            GameOver,

            CountOfStays,

            FromPauseToPlay
        }

        public static Dictionary<WayToSetNewStay, StayOfWork> setOfStay;
        public static StayOfWork currently;

        public static void Start()
        {
            MainWindow.window.Grid.Focus();

            SetStaysValue();
        }

        public static void IterationOfProgramCycle(object sender, EventArgs ea)
        {
            try
            {
                MainWindow.window.PlayingCanvas.Focus();

                WayToSetNewStay wayToSetNewStay = currently.IterationOfProgramCycle();

                if (wayToSetNewStay != WayToSetNewStay.NotSet)
                {
                    switch (wayToSetNewStay)
                    {
                        case WayToSetNewStay.GameOver:
                            (setOfStay[WayToSetNewStay.GameOver] as GameOverStay).SetResultOfPlaying(currently as PlayingStay);

                            currently = setOfStay[WayToSetNewStay.GameOver];

                            currently.Set();
                            break;



                        case WayToSetNewStay.Pause:
                            (setOfStay[WayToSetNewStay.Pause] as PauseStay).SetResultOfPlaying(currently as PlayingStay);

                            currently = setOfStay[WayToSetNewStay.Pause];

                            currently.Set();
                            break;



                        case WayToSetNewStay.FromPauseToPlay:
                            (currently as PauseStay).RemoveToPlay();

                            currently = (currently as PauseStay).currentGameStay;
                            break;



                        default:
                            currently.Remove();

                            currently = setOfStay[wayToSetNewStay];

                            currently.Set();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Exception",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        static void SetStaysValue()
        {
            setOfStay = new Dictionary<WayToSetNewStay, StayOfWork>();

            currently = setOfStay[WayToSetNewStay.StartMenu] = new StartMenuStay();
            setOfStay[WayToSetNewStay.LevelMenu] = new LevelsMenuStay();

            setOfStay[WayToSetNewStay.Level1] = new Level1();
            (setOfStay[WayToSetNewStay.Level1] as PlayingStay).NumberOfLevel = 1;

            setOfStay[WayToSetNewStay.Level2] = new Level2();
            (setOfStay[WayToSetNewStay.Level2] as PlayingStay).NumberOfLevel = 2;

            setOfStay[WayToSetNewStay.Level3] = new Level3();
            (setOfStay[WayToSetNewStay.Level3] as PlayingStay).NumberOfLevel = 3;

            setOfStay[WayToSetNewStay.ListOfSavedGames] = new ListOfSavedGamesStay();

            setOfStay[WayToSetNewStay.SavedGame] = new SavedGame();


            setOfStay[WayToSetNewStay.Pause] = new PauseStay();
            setOfStay[WayToSetNewStay.GameOver] = new GameOverStay();

            currently.Set();
        }

        public static void ProgramKeyDown(object sender, KeyEventArgs eventArgs)
        {
            currently.controlerMessageTurn.push(Message.KeyDown, eventArgs);
        }

        public static TKey KeyOf<TKey, TValue>(this Dictionary<TKey, TValue> dic, TValue val)
        {
            return dic.FirstOrDefault(x => x.Value.Equals(val)).Key;
        }
    }
}
