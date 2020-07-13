using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using WayToEarth.GameLogic;

namespace WayToEarth.StaysOfWork
{
    partial class PlayingStay : StayOfWork
    {
        public GameObject centralObject;

        public ReactiveGases fire;

        public double scale = 1;

        public override void StartVisio()
        {
            centralObject = rocket;
        }





        public override void Visio()
        {
            SetPositionOfAllObjects(gModel.gObjects, centralObject);
        }





        void SetPositionOfAllObjects(List<GameObject> objects, GameObject central)
        {
            foreach (GameObject go in objects)
            {
                PointF canvCoord = LogicToCanvasCoord(go, central);

                Canvas.SetTop(go.image, canvCoord.Y);
                Canvas.SetLeft(go.image, canvCoord.X);

                RotateTransform rotate = new RotateTransform(go.Angle);
                go.image.RenderTransform = rotate;
            }
        }





        PointF LogicToCanvasCoord(GameObject go, GameObject central)
        {
            MainWindow window = MainWindow.window;
            Canvas canvas = window.PlayingCanvas;

            double wObj = go.Width;
            double hObj = go.Height;

            if (!go.isVisible)
                return new PointF((float)-wObj, (float)-hObj);

            double wCanv = canvas.ActualWidth;
            double hCanv = canvas.ActualHeight;

            double xCentr = wCanv / 2;
            double yCentr = hCanv / 2;

            double defX = go.X - central.X;
            double defY = go.Y - central.Y;

            double defXbyScale = defX / scale;
            double defYbyScale = defY / scale;

            double centrObjX = xCentr + defXbyScale;
            double centrObjY = yCentr + defYbyScale;

            double resultX = centrObjX - wObj / 2;
            double resultY = centrObjY - hObj / 2;

            return new PointF((float)resultX, (float)resultY);
        }




        void VisioRemove()
        {
            foreach (GameObject go in gModel.gObjects)
            {
                go.isValid = false;
                go.isVisible = false;
            }

            SetPositionOfAllObjects(gModel.gObjects, centralObject);

            gModel.DeleteInvalidObjects();
        }
    }
}
