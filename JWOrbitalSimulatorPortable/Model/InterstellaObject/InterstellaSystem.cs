using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


// ~~ TODO: Impliment Collision detection and resolution too solve the slingshot off the screen problem when two objects pass through each other
// ## TODO: Dont consider the forces of objects off of the screen, I may want to reverse this if i ever impliment any form of zoom or scrolling to expand the screen

namespace JWOrbitalSimulatorPortable.Model
{
    public class InterstellaSystem
    {
        private double G = 6.674E-11;

        // ## All 3 of the below values impact the speed and physics of the model, dtScaler appears to be the one which is easiest to use to scale
        private int _TimerInterval = 1;
        private int _dtScaler = 50;

        // ~~ This Value still has a mature impact on the physics of the model
        private double _SystemAccelerationMultiplier = 1051333E6;

        private Timer _SystemClock;

        private int _SystemWidth = (int)CanvasPageViewModel.SeperationScaler(1200);
        private int _SystemHight = (int)CanvasPageViewModel.SeperationScaler(700);

        public InterstellaSystem() { }

        public InterstellaSystem(List<InterstellaObject> interstellaObjects)
        {
            InterstellaObjects = interstellaObjects;


        }

        public List<InterstellaObject> InterstellaObjects { get; set; } = new List<InterstellaObject>();

        public int SystemWidth { get => _SystemWidth; set => _SystemWidth = value; }
        public int SystemHight { get => _SystemHight; set => _SystemHight = value; }

        public void Start() => _SystemClock = new Timer(new TimerCallback(update), null, 0, _TimerInterval);

        public void Stop() => _SystemClock?.Dispose();


        public void Step()
        {
            update(new object());
        }

        public void AddObject(InterstellaObject newIntestellaObject) => InterstellaObjects.Add(newIntestellaObject);

        private void update(object timerState)
        {
            foreach (var ResolvableObject in InterstellaObjects)
            {
                Vector ResultantForceAtUpdate = new Vector(0, 0);

                // Find the sum of all forces exerting on the resolvable object
                foreach (var ForceExcertingObject in InterstellaObjects)
                {
                    if (ForceExcertingObject == ResolvableObject) continue;

                    ResultantForceAtUpdate += gForceVector(ResolvableObject, ForceExcertingObject);
                }

                // Replace the old Force with the one at time of update
                ResolvableObject.ResultantForce = ResultantForceAtUpdate;
            }

            try
            {
                foreach (var Object in InterstellaObjects)
                {
                    // Give Resultant acceleration too the Object

                    // ~~ Try to replace System Speed Multiplier with a increase in TimeStep while maintaining accuracy, if possible
                    Vector Acceleration = (Object.ResultantForce / Object.Mass) * _SystemAccelerationMultiplier;

                    Object.Update(_TimerInterval* _dtScaler, Acceleration);
                }
            }
            catch (InvalidOperationException)
            {
                // If Collection is modified during Update drop this update
                // This Solves an error that occurs if the user happens to drop and object during an update
            }

        }

        private Vector gForceVector(InterstellaObject ResolvableObject, InterstellaObject ExcertingObject)
        {
            // Find the Vetor from the Excerting Object too the resolvable object
            Vector R1R2 = ResolvableObject.Position - ExcertingObject.Position;

            // The Unit Vector of R1R2 is the vector over its magnitude
            Vector R1R2Unit = R1R2 / R1R2.Magnitude;

            // - G (m1m2)/|r|^2 * UnitVector
            return new Vector(-G * ((ResolvableObject.Mass * ExcertingObject.Mass) / Math.Pow(R1R2.Magnitude, 2)) * R1R2Unit);
        }
    }
}
