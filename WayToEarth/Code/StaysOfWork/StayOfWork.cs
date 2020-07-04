using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections;
using static WayToEarth.MainProgramWork;

namespace WayToEarth.StaysOfWork
{
    abstract class StayOfWork
    {

        protected internal MessageTurn controlerMessageTurn;
        protected internal MessageTurn logicMessageTurn;
        protected internal MessageTurn visioMessageTurn;

        public virtual WayToSetNewStay IterationOfProgramCycle()
        {
            Controller();

            WayToSetNewStay outMes = Logic();

            Visio();

            if (outMes != WayToSetNewStay.NotSet) return outMes;

            return WayToSetNewStay.NotSet;
        }

        public abstract void Set();

        public abstract void Remove();

        public abstract void StartController();

        public abstract void Controller();

        public abstract void StartLogic();

        public abstract WayToSetNewStay Logic();

        public abstract void StartVisio();

        public abstract void Visio();

    }
}
