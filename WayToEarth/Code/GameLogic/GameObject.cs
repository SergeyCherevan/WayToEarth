using Newtonsoft.Json;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Controls;
using static WayToEarth.GameLogic.GameObject;

namespace WayToEarth.GameLogic
{
    public class GameObject
    {
        virtual public bool isValid { get; set; }

        [JsonIgnore]
        public GameModel model;

        [JsonIgnore]
        public Image image;

        public string ImageName
        {
            get => ImageNameMap.GetName(image);
            set { image = ImageNameMap.GetImage(value); }
        }

        virtual public Complex Coord { get; set; }

        virtual public double X {
            get 
            {
                return Coord.Real;
            }
            set 
            {
                Coord = new Complex(value, Coord.Imaginary);
            } 
        }

        virtual public double Y
        {
            get
            {
                return Coord.Imaginary;
            }
            set
            {
                Coord = new Complex(Coord.Real, value);
            }
        }

        virtual public double Angle { get; set; }

        virtual public double Width { get { return image?.ActualWidth ?? 0; } }
        virtual public double Height { get { return image?.ActualHeight ?? 0; } }
        virtual public Complex Size { get { return new Complex(Width, Height); } }
        virtual public double Radius { get { return Complex.Abs(Size) / 2; } }

        virtual public bool isVisible { get; set; }


        public delegate void gInteraction(GameObject go1, GameObject go2, double timeInSec);
        public delegate void gAction(GameObject go, double timeInSec);
        public delegate bool InteractCondition(GameObject go1, GameObject go2, double timeInSec);
        public delegate bool ActCondition(GameObject go, double timeInSec);

        [JsonIgnore]
        public virtual gInteraction InteractionWithAll { get; set; }
        [JsonIgnore]
        public virtual gAction ActionAlwaysBeforeIntract { get; set; }
        [JsonIgnore]
        public virtual gAction ActionAlwaysAfterIntract { get; set; }
        [JsonIgnore]
        public virtual gAction ActionAlwaysAfterPhisic { get; set; }

        [JsonIgnore]
        public virtual List<KeyValuePair<InteractCondition, gInteraction>> InteractToCondit { get; set; }
        [JsonIgnore]
        public virtual List<KeyValuePair<ActCondition, gAction>> ActToConditBeforeIntract { get; set; }
        [JsonIgnore]
        public virtual List<KeyValuePair<ActCondition, gAction>> ActToConditAfterIntract { get; set; }
        [JsonIgnore]
        public virtual List<KeyValuePair<ActCondition, gAction>> ActToConditAfterPhisic { get; set; }

        public virtual string strInteractionWithAll { get; set; }
        public virtual string strActionAlwaysBeforeIntract { get; set; }
        public virtual string strActionAlwaysAfterIntract { get; set; }
        public virtual string strActionAlwaysAfterPhisic { get; set; }

        public virtual List<KeyValuePair<string, string>> strInteractToCondit { get; set; }
        public virtual List<KeyValuePair<string, string>> strActToConditBeforeIntract { get; set; }
        public virtual List<KeyValuePair<string, string>> strActToConditAfterIntract { get; set; }
        public virtual List<KeyValuePair<string, string>> strActToConditAfterPhisic { get; set; }

        public GameObject()
        {
            isValid = true;

            model = null;

            NullGDelegate.SetGDelegateValue(this);
        }
    }



    class NullGDelegate
    {
        public static void SetGDelegateValue(GameObject o)
        {
            o.InteractionWithAll = null;

            o.ActionAlwaysBeforeIntract = null;
            o.ActionAlwaysAfterIntract = null;
            o.ActionAlwaysAfterPhisic = null;

            o.InteractToCondit = new List<KeyValuePair<InteractCondition, gInteraction>>();

            o.ActToConditBeforeIntract = new List<KeyValuePair<ActCondition, gAction>>();
            o.ActToConditAfterIntract = new List<KeyValuePair<ActCondition, gAction>>();
            o.ActToConditAfterPhisic = new List<KeyValuePair<ActCondition, gAction>>();
        }
    }
}
