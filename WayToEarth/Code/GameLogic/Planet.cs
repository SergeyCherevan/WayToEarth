namespace WayToEarth.GameLogic
{
    public class Planet : PhisicSimulatedGameObj
    {
        public override double Radius { get { return (image?.ActualHeight ?? 0) / 2; } }

        public Planet() : base()
        {
            phisObj = new RigidBody();

            phisObj.mass = 1e+14;

            Angle = 0;
        }
    }
}
