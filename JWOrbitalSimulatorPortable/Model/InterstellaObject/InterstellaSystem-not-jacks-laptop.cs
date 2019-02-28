using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


// ## TODO: Impliment Collision detection and resolution too solve the slingshot off the screen problem when two objects pass through each other
// ## TODO: Dont consider the forces of objects off of the screen, I may want to reverse this if i ever impliment any form of zoom or scrolling to expand the screen

namespace JWOrbitalSimulatorPortable.Model
{
    public class InterstellaSystem
    {
        private double G = 6.674E-11;

        private int _TimerInterval = 24;

        // ## TODO: Apply the Scaler to acceleration not time, 
        // time between updates should be short enough that change in accleration over that frame can be considered neglible
        // By Applying to it to acceleration, theoretically everything in the model just moves at a scaled up speed
        private double _TimeScaler = 1051333;

        private Timer _SystemClock;

        private int _SystemWidth = (int)CanvasPageViewModel.InverseScale(1200, CanvasPageViewModel.BaseScale, CanvasPageViewModel.PositionScale);
        private int _SystemHight = (int)CanvasPageViewModel.InverseScale(700, CanvasPageViewModel.BaseScale, CanvasPageViewModel.PositionScale);

        public InterstellaSystem() { }

        public InterstellaSystem(List<InterstellaObject> interstellaObjects)
        {
            InterstellaObjects = interstellaObjects;


        }

        public List<InterstellaObject> InterstellaObjects { get; set; } = new List<InterstellaObject>();

        public int SystemWidth { get => _SystemWidth; set => _SystemWidth = value; }
        public int SystemHight { get => _SystemHight; set => _SystemHight = value; }

        public void Start()
        {
            _SystemClock = new Timer(new TimerCallback(update), null, 0, _TimerInterval);
        }

        public void Stop()
        {
           _SystemClock?.Dispose();
        }

        public void Step()
        {
            update(new object());
        }

        public void AddObject(InterstellaObject newIntestellaObject) => InterstellaObjects.Add(newIntestellaObject);

        private void update(object timerState)
        {
            foreach (var ResolvableObject in InterstellaObjects)
            {
                foreach (var ForceExcertingObject in InterstellaObjects)
                {
                    if (ForceExcertingObject == ResolvableObject) continue;

                    // Resolvable Object is the earth r1
                    // Force Excerting object is the sun r2

                    // R1 to R2 is given by R2 - R1.
                    Vector R1R2 = ForceExcertingObject.Position - ResolvableObject.Position;

                    double magntiudeR1R2 = Math.Sqrt(Math.Pow(R1R2.X, 2) + Math.Pow(R1R2.Y, 2));

                    Vector UnitVector = R1R2 / magntiudeR1R2;

                    // If both objects are the same do not resolve forces as the object does not resolve force on its self.

                    // Calculate Vector Resulting force due to gravity
                    double MagnitudeOfGrav = -G * ( (ResolvableObject.Mass * ForceExcertingObject.Mass) / Math.Pow(magntiudeR1R2, 2) );

                    Vector GravVector = MagnitudeOfGrav * UnitVector;

                    ResolvableObject.ResultantForce -= GravVector;
                }
            }

            try
            {
                foreach (var Object in InterstellaObjects)
                {
                    Object.Stopwatch.Stop();

                    // Give Resultant acceleration too the Object

                    // Scaling up acceleration applied rather than time passed to increase the speed of the model
                    Vector Acceleration = (Object.ResultantForce / Object.Mass) * _TimeScaler*10000;

                    //Object.Update(Object.Stopwatch.ElapsedMilliseconds * _TimeScaler, Acceleration);

                    Object.Update(_TimerInterval, Acceleration);

                    Object.Stopwatch.Reset();
                    Object.Stopwatch.Start();
                }
            }
            catch (InvalidOperationException)
            {
                // If Collection is modified during Update drop this update
                // This Solves an error that occurs if the user happens to drop and object during an update
            }

        }

        private double scale(double length) => CanvasPageViewModel.Scale(length, CanvasPageViewModel.BaseScale, CanvasPageViewModel.SizeScale);
    }
}
