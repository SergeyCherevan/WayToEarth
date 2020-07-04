﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WayToEarth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public MainWindow window;

        static public int iterat = 0;

        public MainWindow()
        {
            InitializeComponent();

            window = this;

            MainProgramWork.Start();

            DispatcherTimer t = new DispatcherTimer();
            t.Tick += new EventHandler(MainProgramWork.IterationOfProgramCycle);
            t.Interval = TimeSpan.FromMilliseconds(50);
            t.Start();
        }

        public void MainKeyDown(object sender, KeyEventArgs eventArgs)
        {
            MainProgramWork.ProgramKeyDown(sender, eventArgs);
        }

        public static T Clone<T>(T controlToClone)// where T : Control
        {
            T instance = Activator.CreateInstance<T>();

            Type control = controlToClone.GetType();
            PropertyInfo[] info = control.GetProperties();
            object p = control.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, controlToClone, null);
            foreach (PropertyInfo pi in info)
            {
                if ((pi.CanWrite) && !(pi.Name == "WindowTarget") && !(pi.Name == "Capture"))
                {
                    pi.SetValue(instance, pi.GetValue(controlToClone, null), null);
                }
            }
            return instance;
        }
    }
}
