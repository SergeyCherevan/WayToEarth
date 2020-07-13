using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using WayToEarth.GameLogic;
using WayToEarth.Phisic;

namespace WayToEarth.StaysOfWork
{
    public partial class PlayingStay : StayOfWork
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

            Coord whObj = go.Size;

            if (!go.isVisible)
                return new PointF((float)-whObj.x, (float)-whObj.y);

            Coord cCanv = new Coord(canvas.ActualWidth, canvas.ActualHeight);

            Coord cCentr = cCanv / 2;

            Coord def = go.Coord - central.Coord;

            Coord defByScale = def / scale;

            Coord byCentrObj = cCentr + defByScale;

            Coord result = byCentrObj - whObj / 2;

            return new PointF((float)result.x, (float)result.y);
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
