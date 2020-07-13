using Newtonsoft.Json;
using System.Collections.Generic;
using static WayToEarth.Phisic.PhisicalObject;
using Action = WayToEarth.Phisic.PhisicalObject.Action;

namespace WayToEarth.Phisic
{
    public class RotatCharacter : PhisicalObject
    {
        public RotatCharacter rc
        {
            get => this;
            set
            {
                angle = value.angle;
                angulVel = value.angulVel;
                inertMoment = value.inertMoment;
            }
        }

        public double angle { get; set; }
        public double angulVel { get; set; }
        public double inertMoment { get; set; }



        [JsonIgnore]
        public Interaction InteractionWithAll { get; set; }
        [JsonIgnore]
        public Action ActionAlways { get; set; }

        [JsonIgnore]
        public List<KeyValuePair<InteractCondition, Interaction>> InteractToCondit { get; set; }
        [JsonIgnore]
        public List<KeyValuePair<ActCondition, Action>> ActToCondit { get; set; }


        public RotatCharacter()
        {
            angle = angulVel = 0;
            inertMoment = 1;
        }

        public RotatCharacter(double Angle, double AngularVelocity, double InertionMoment)
        {
            angle = Angle;
            angulVel = AngularVelocity;
            inertMoment = InertionMoment;
        }

        public RotatCharacter Move(double timeInSec)
        {
            angle += angulVel * timeInSec;
            return this;
        }

        public RotatCharacter AddAcceleration(double e, double timeInSec)
        {
            angulVel += e * timeInSec;
            return this;
        }

        public RotatCharacter ActMomentOfForce(double M, double timeInSec)
        {
            AddAcceleration(M / inertMoment, timeInSec);
            return this;
        }

        public RotatCharacter AddMomentOfImpulse(double L)
        {
            angulVel += L / inertMoment;
            return this;
        }
    }
}
