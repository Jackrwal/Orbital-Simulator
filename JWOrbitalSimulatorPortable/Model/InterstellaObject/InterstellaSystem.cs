using System;
using System.Collections.Generic;
using System.Threading;

namespace JWOrbitalSimulatorPortable.Model
{
    /// <summary>
    /// Models the gravitational attraction between a collection of interstellarObject
    /// </summary>
    public class InterstellaSystem
    {
        // Newtons Constant of gravitation
        public const double G = 6.674E-11;
       
        // Interval between timer ticks to update.
        private const int _TimerInterval = 24;

        // Multiplier of time passed between ticks used to increae the run speed of the system
        private double _dtScaler = 1E6;
        
        /// <summary>
        /// Raised when the collection of interstellar Objects in the system is altered
        /// To allow the display update appropriatly to removed or added objects.
        /// </summary>
        public event EventHandler SystemCollectionAltered;

        // The percentage of combined mass retaine by the larger object in the event of a collission
        public double CollissionMassRetentionPercentage = 0.8;

        // Controls whether the timer causing the system to update is currently running
        private bool _SysRunning = false;
        public bool RunSys { get { return _SysRunning; } set { if (value) Start(); else Stop(); } }

        // The Timer used to pirodically update the system
        private Timer _SysClock;

        // The Name to be used in save files for this system
        public string SystemSaveName { get; set; }

        // Blank constructor allowing an instance to be created with no paramaters
        public InterstellaSystem() { }

        // Constuct an instance of this class with a list of objcets to add the system.
        public InterstellaSystem(List<InterstellaObject> interstellaObjects)
        {
            InterstellaObjects = interstellaObjects;
        }

        // The InterstellarObjects in this system.
        public List<InterstellaObject> InterstellaObjects { get; set; } = new List<InterstellaObject>();

        // Encapsulation of the time scaler.
        public double TimeMultiplier { get => _dtScaler; set => _dtScaler = value; }

        public void Start()
        {
            // if the system is already running do nothing.
            if (_SysRunning) return;

            // set the timer to update the system at the set time interval and flag the system as running
            _SysClock = new Timer(new TimerCallback(update), null, 0, _TimerInterval); 
            _SysRunning = true;
        }
        
        public void Stop()
        {
            // If the system is already stopped do nothing
            if (!_SysRunning) return;

            // If there is a timer object dispose of it. and flag the system as not running
            _SysClock?.Dispose();
            _SysRunning = false;
        }

        // add or remove an object from the system.
        public void AddObject(InterstellaObject newIntestellaObject)
        {
            InterstellaObjects.Add(newIntestellaObject);

            // When objects added or removed let any subscribers know the systme collection has changed
            SystemCollectionAltered?.Invoke(this, new EventArgs());
        }
        public void RemoveObject(InterstellaObject Object)
        {
            SystemCollectionAltered?.Invoke(this, new EventArgs());
            InterstellaObjects.Remove(Object);
        }

        /// <summary>
        /// calculates the resultant force on each object in the system due to gravitational attraction
        /// and applies it to each object before updating them.
        /// </summary>
        private void update(object timerState)
        {
            // Store objects to be removed until enumeration of the collection is complete
            List<InterstellaObject> DeleteableObjects = new List<InterstellaObject>();
            try
            { 
                // Resolve the force on every object in the system
                foreach (var ResolvableObject in InterstellaObjects)
                {
                    // 
                    Vector ResultantForceAtUpdate = (0, 0);

                    // Find the sum of all forces exerted on resolvable object due to each other object.
                    foreach (var ForceExcertingObject in InterstellaObjects)
                    {
                        // Do not attempt to resolve forces excerted on self. 
                        // As you can imagine this caused very wierd issues and was a hard one to find
                        if (ForceExcertingObject == ResolvableObject) continue;

                        // Check for two intersecting objects
                        if ((ResolvableObject.Position - ForceExcertingObject.Position).Magnitude < (ResolvableObject.Radius + ForceExcertingObject.Radius))
                        {
                            // Resolve the collission by keeping the larger object with a percentage of the two intersecting objects combined mass
                            if (ResolvableObject.Mass > ForceExcertingObject.Mass)
                            {
                                ResolvableObject.Mass = (ResolvableObject.Mass + ForceExcertingObject.Mass) * CollissionMassRetentionPercentage;
                                DeleteableObjects.Add(ForceExcertingObject);
                            }
                            else
                            {
                                ForceExcertingObject.Mass = (ResolvableObject.Mass + ForceExcertingObject.Mass) * CollissionMassRetentionPercentage;
                                DeleteableObjects.Add(ResolvableObject);
                            }
                        }
                        // calculate the force 
                        ResultantForceAtUpdate += gForceVector(ResolvableObject, ForceExcertingObject);
                    }

                    // Replace the old Force with the one at time of update
                    ResolvableObject.ResultantForce = ResultantForceAtUpdate;
                }
                // Update Objects with their new forces.
                foreach (var Object in InterstellaObjects)
                {
                    // Calculate accelleration on the object
                    Vector Acceleration = (Object.ResultantForce / Object.Mass);
                    // Update each object. Elapsed time is scaled to accelerate the model for visible orbits.
                    Object.Update(_TimerInterval* _dtScaler, Acceleration);
                }

                // Clean Up Objects removed due to collission
                if (DeleteableObjects.Count > 0)
                {
                    foreach (var ObjectToDelete in DeleteableObjects)
                        RemoveObject(ObjectToDelete);
                }

            }
            catch (InvalidOperationException)
            {
                // If the collection is modified by the UI Thread during this update drop the update
            }
        }

        /// <summary>
        /// Calculates the Vector of gravitational force on the resolvable object by an excerting object
        /// </summary>
        private Vector gForceVector(InterstellaObject ResolvableObject, InterstellaObject ExcertingObject)
        {
            // Find the Vetor from the Excerting Object too the resolvable object.
            // Magnitude is used for speration between the two objects. 
            // Direction is used for the direction of the resulting force.
            Vector R1R2 = ResolvableObject.Position - ExcertingObject.Position;

            // The Unit Vector of R1R2 is the vector over its magnitude
            // This is used to give a direction to the calculated magnitude of force.
            Vector R1R2Unit = R1R2 / R1R2.Magnitude;

            // - G (m1m2)/|r|^2 * UnitVector
            return new Vector(-G * ((ResolvableObject.Mass * ExcertingObject.Mass) / Math.Pow(R1R2.Magnitude, 2)) * R1R2Unit);
        }
    }
}