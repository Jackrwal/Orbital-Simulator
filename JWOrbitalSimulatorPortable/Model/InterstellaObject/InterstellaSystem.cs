using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public class InterstellaSystem
    {
        public double G = 6.674E-11;
       
        private int _TimerInterval = 24;
        private double _dtScaler = 1E6;
        
        public event EventHandler SystemCollectionAltered;

        public double CollissionMassRetentionPercentage = 0.8;

        private bool _SysRunning = false;
        public bool RunSys { get { return _SysRunning; } set { if (value) Start(); else Stop(); } }

        private Timer _SysClock;

        public InterstellaSystem() { }

        public InterstellaSystem(List<InterstellaObject> interstellaObjects)
        {
            InterstellaObjects = interstellaObjects;

        }

        public List<InterstellaObject> InterstellaObjects { get; set; } = new List<InterstellaObject>();

        public double TimeMultiplier { get => _dtScaler; set => _dtScaler = value; }

        public void Start()
        {
            if (_SysRunning) return;
            _SysClock = new Timer(new TimerCallback(update), null, 0, _TimerInterval); 
            _SysRunning = true;
        }
        
        public void Stop()
        {
            if (!_SysRunning) return;
            _SysClock?.Dispose();
            _SysRunning = false;
        }

        public void Step()
        {
            update(new object());
        }

        public void AddObject(InterstellaObject newIntestellaObject) => InterstellaObjects.Add(newIntestellaObject);
        public void RemoveObject(InterstellaObject Object) => InterstellaObjects.Remove(Object);

        private void update(object timerState)
        {
            List<InterstellaObject> DeleteableObjects = new List<InterstellaObject>();

            // ## I should really solve this by temporarily storing new objects and adding them at the end of the update instead of dropping the update..
            try
            { 
                foreach (var ResolvableObject in InterstellaObjects)
                {
                    Vector ResultantForceAtUpdate = new Vector(0, 0);

                    // Find the sum of all forces exerting on the resolvable object
                    foreach (var ForceExcertingObject in InterstellaObjects)
                    {
                        if (ForceExcertingObject == ResolvableObject) continue;

                        // ## Im hacking collissions by multiplying the sum of their radii to make it more likley two objects colide..
                        if ((ResolvableObject.Position - ForceExcertingObject.Position).Magnitude < (ResolvableObject.Radius + ForceExcertingObject.Radius)*10)
                        {
                            // Keep the larger object on the canvas
                            if (ResolvableObject.Mass > ForceExcertingObject.Mass)
                            {
                                // Keep Density Constant by increasing radius of new object
                                double RetainedDencity = ResolvableObject.Density;

                                ResolvableObject.Mass = (ResolvableObject.Mass + ForceExcertingObject.Mass) * CollissionMassRetentionPercentage;
                                DeleteableObjects.Add(ForceExcertingObject);
                                // If the Mass is greater than a limiting value the object's type may change (Red-Giant, Supernoave etc.)
                            }
                            else
                            {
                                ForceExcertingObject.Mass = (ResolvableObject.Mass + ForceExcertingObject.Mass) * CollissionMassRetentionPercentage;
                                DeleteableObjects.Add(ResolvableObject);
                            }
                        }

                        ResultantForceAtUpdate += gForceVector(ResolvableObject, ForceExcertingObject);
                    }

                    // Replace the old Force with the one at time of update
                    ResolvableObject.ResultantForce = ResultantForceAtUpdate;
                }
                // Update Objects with their new forces.
                foreach (var Object in InterstellaObjects)
                {
                    // Give Resultant acceleration too the Object

                    Vector Acceleration = (Object.ResultantForce / Object.Mass);

                    Object.Update(_TimerInterval* _dtScaler, Acceleration);
                }

                // Clean Up Removed Objects
                if (DeleteableObjects.Count > 0)
                {
                    foreach (var ObjectToDelete in DeleteableObjects)
                        RemoveObject(ObjectToDelete);

                    SystemCollectionAltered(this, new EventArgs());
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