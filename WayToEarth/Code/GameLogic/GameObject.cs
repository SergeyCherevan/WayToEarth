using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Controls;
using WayToEarth.Phisic;
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

        virtual public Coord Coord { get; set; }

        public double X
        {
            get
            {
                GameObject go = this;
                return go.Coord.x;
            }
            set
            {
                GameObject go = this;
                go.Coord.x = value;
            }
        }

        public double Y
        {
            get
            {
                GameObject go = this;
                return go.Coord.y;
            }
            set
            {
                GameObject go = this;
                go.Coord.y = value;
            }
        }

        virtual public double Angle { get; set; }

        virtual public double Width { get { return image?.ActualWidth ?? 0; } }
        virtual public double Height { get { return image?.ActualHeight ?? 0; } }
        virtual public Coord Size { get { return new Coord(Width, Height); } }
        virtual public double Radius { get { return Size.polarR / 2; } }

        virtual public bool isVisible { get; set; }


        public delegate void Interaction(GameObject go1, GameObject go2, double timeInSec);
        public delegate void Action(GameObject go, double timeInSec);
        public delegate bool InteractCondition(GameObject go1, GameObject go2, double timeInSec);
        public delegate bool ActCondition(GameObject go, double timeInSec);

        [JsonIgnore]
        public Interaction InteractionWithAll;
        [JsonIgnore]
        public Action ActionAlwaysBeforeIntract;
        [JsonIgnore]
        public Action ActionAlwaysAfterIntract;
        [JsonIgnore]
        public Action ActionAlwaysAfterPhisic;

        [JsonIgnore]
        public List<KeyValuePair<InteractCondition, Interaction>> InteractToCondit;
        [JsonIgnore]
        public List<KeyValuePair<ActCondition, Action>> ActToConditBeforeInteract;
        [JsonIgnore]
        public List<KeyValuePair<ActCondition, Action>> ActToConditAfterIntract;
        [JsonIgnore]
        public List<KeyValuePair<ActCondition, Action>> ActToConditAfterPhisic;

        public virtual string strInteractionWithAll
        {
            get => InteractionWithAll.NameInMap();
            set { InteractionWithAll = value.MethodInMap<GameObject.Interaction>(); }
        }
        public virtual string strActionAlwaysBeforeIntract
        {
            get => ActionAlwaysBeforeIntract.NameInMap();
            set { ActionAlwaysBeforeIntract = value.MethodInMap<GameObject.Action>(); }
        }
        public virtual string strActionAlwaysAfterIntract
        {
            get => ActionAlwaysAfterIntract.NameInMap();
            set { ActionAlwaysAfterIntract = value.MethodInMap<GameObject.Action>(); }
        }
        public virtual string strActionAlwaysAfterPhisic
        {
            get => ActionAlwaysAfterPhisic.NameInMap();
            set { ActionAlwaysAfterPhisic = value.MethodInMap<GameObject.Action>(); }
        }

        public virtual string strInteractToCondit
        {
            get => InteractToCondit.MethodsPairsToNamesP();
            set
            {
                InteractToCondit = value.NamesPairsToMethodsP<
                        GameObject.InteractCondition,
                        GameObject.Interaction
                    >();
            }
        }
        public virtual string strActToConditBeforeIntract
        {
            get => ActToConditBeforeInteract.MethodsPairsToNamesP();
            set
            {
                ActToConditBeforeInteract = value.NamesPairsToMethodsP<
                        GameObject.ActCondition,
                        GameObject.Action
                    >();
            }
        }
        public virtual string strActToConditAfterIntract
        {
            get => ActToConditAfterIntract.MethodsPairsToNamesP();
            set
            {
                ActToConditAfterIntract = value.NamesPairsToMethodsP<
                        GameObject.ActCondition,
                        GameObject.Action
                    >();
            }
        }
        public virtual string strActToConditAfterPhisic
        {
            get => ActToConditAfterPhisic.MethodsPairsToNamesP();
            set
            {
                ActToConditAfterPhisic = value.NamesPairsToMethodsP<
                        GameObject.ActCondition,
                        GameObject.Action
                    >();
            }
        }

        public GameObject()
        {
            isValid = true;

            if (this.GetType() == typeof(GameObject))
            {
                GameObject go = this;
                go.Coord = new Coord();
            }
            

            model = null;

            NullGDelegate.SetGDelegateValue(this);
        }
    }



    public class NullGDelegate
    {
        public static void SetGDelegateValue(GameObject o)
        {
            o.InteractionWithAll = null;

            o.ActionAlwaysBeforeIntract = null;
            o.ActionAlwaysAfterIntract = null;
            o.ActionAlwaysAfterPhisic = null;

            o.InteractToCondit = new List<KeyValuePair<InteractCondition, Interaction>>();

            o.ActToConditBeforeInteract = new List<KeyValuePair<ActCondition, Action>>();
            o.ActToConditAfterIntract = new List<KeyValuePair<ActCondition, Action>>();
            o.ActToConditAfterPhisic = new List<KeyValuePair<ActCondition, Action>>();
        }
    }
}
