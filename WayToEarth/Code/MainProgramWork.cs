using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WayToEarth.StaysOfWork;
using WayToEarth.StaysOfWork.Levels;
using System.Linq;
using WayToEarth.GameLogic;
using Newtonsoft.Json;
using WayToEarth.Code;
using WayToEarth.Phisic;

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
            try
            {
                MainWindow.window.Grid.Focus();

                SetValueOfStaticMaps();

                SetStaysValue();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            } 
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

                            var obj = (currently as PlayingStay).rocket.InteractToCondit.MethodsPairsToNamesP();

                            /*MessageBox.Show(
                                    System.Text.Json.JsonSerializer.Serialize(obj, obj.GetType(), 
                                        new System.Text.Json.JsonSerializerOptions() { WriteIndented = true })
                                );*/

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
                MessageBox.Show(e.ToString(), "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        static void SetValueOfStaticMaps()
        {
            StarterNameMap.Start(new ImageNameMap());

            StarterNameMap.Start(new MethodNameMap<GameObject.Action>());

            StarterNameMap.Start(new MethodNameMap<GameObject.Interaction>());

            StarterNameMap.Start(new MethodNameMap<GameObject.ActCondition>());

            StarterNameMap.Start(new MethodNameMap<GameObject.InteractCondition>());

            StarterNameMap.Start(new MethodNameMap<PhisicalObject.Action>());

            StarterNameMap.Start(new MethodNameMap<PhisicalObject.Interaction>());

            StarterNameMap.Start(new MethodNameMap<PhisicalObject.ActCondition>());

            StarterNameMap.Start(new MethodNameMap<PhisicalObject.InteractCondition>());
        }

        public static void ProgramKeyDown(object sender, KeyEventArgs eventArgs)
        {
            currently.controlerMessageTurn.push(Message.KeyDown, eventArgs);
        }

        public static TKey KeyOf<TKey, TValue>(this Dictionary<TKey, TValue> dic, TValue val)
        {
            return dic.FirstOrDefault(x => Object.Equals(x.Value, val)).Key;
        }
    }
}
