using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public class InterstellaSystem
    {
        private double G = 6.674E-11;

        private int _TimerInterval = 24;

        // Use scaler of 1,051,333 for a year in 30s
        private double _TimeScaler = 1051333;

        private Timer _SystemClock;

        public InterstellaSystem() { }

        public InterstellaSystem(List<InterstellaObject> interstellaObjects)
        {
            InterstellaObjects = interstellaObjects;
        }

        public List<InterstellaObject> InterstellaObjects { get; set; } = new List<InterstellaObject>();

        public void Start()
        {
            _SystemClock = new Timer(new TimerCallback(update), null, 0, _TimerInterval);
        }

        public void Stop()
        {
            _SystemClock.Dispose();
        }

        public void AddObject(InterstellaObject newIntestellaObject) => InterstellaObjects.Add(newIntestellaObject);

        private void update(object timerState)
        {
            foreach (var ResolvableObject in InterstellaObjects)
            {
                foreach (var ForceExcertingObject in InterstellaObjects)
                {
                    // If both objects are the same do not resolve forces as the object does not resolve force on its self.
                    if (ForceExcertingObject == ResolvableObject) continue;

                    // The Adjacent of the triangle is change in x
                    double Adj = ResolvableObject.Position.X - ForceExcertingObject.Position.X;

                    // The Opposite of the triangle Change in Y
                    double Opp = ResolvableObject.Position.Y - ForceExcertingObject.Position.Y;

                    // The Hypotinuse is given by pythagorus of a and b
                    double Hyp = Math.Sqrt(Math.Pow(Adj, 2) + Math.Pow(Opp, 2));

                    // Tan(Theta) = Adj/Opp
                    // Cos(Theta) = Adj/Hyp
                    // Sin(Theta) = Opp/Hyp

                    // F = G(m1m2)/r^2
                    double MagnitudeOfGraviationalForce = (G * (ResolvableObject.Mass * ForceExcertingObject.Mass)) / Math.Pow(Hyp,2);

                    // F Horiz = FCos(Theta), Cos(Theta) = a/c
                    double HorizontalForce = MagnitudeOfGraviationalForce * (Adj/Hyp);

                    // F Vert = FSin(Theta), Sin(Theta) = b/c
                    double VerticalForce = MagnitudeOfGraviationalForce * (Opp/Hyp);

                    ResolvableObject.ResultantForce -= new Vector(HorizontalForce, VerticalForce);

                    // Now that i have calculated the force of the Force Exerting Object on the ResolvableObject
                    // I can also add the force to the Force Exerting Object as well as the resolvable object
                    // however this means when the Force Exerting Object is the Resolvable object
                }
            }

            foreach (var Object in InterstellaObjects)
            {
                Object.Stopwatch.Stop();

                // Give Resultant acceleration too the Object

                if (Object.ResultantForce.Resultant == 0) Object.Update(Object.Stopwatch.ElapsedMilliseconds * _TimeScaler);
                else
                {
                    Vector Acceleration = new Vector((Object.ResultantForce.X / Object.Mass), (Object.ResultantForce.Y / Object.Mass));
                    Object.Update(Object.Stopwatch.ElapsedMilliseconds * _TimeScaler, Acceleration);
                }

                Object.Stopwatch.Reset();
                Object.Stopwatch.Start();
            }
        }
    }
}
