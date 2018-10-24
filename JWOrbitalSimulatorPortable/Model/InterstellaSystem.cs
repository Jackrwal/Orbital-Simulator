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
        private int _TimerInterval = 24;

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
            foreach (var Object in InterstellaObjects)
            {
                // Resolve Forces of the system on this object
                // Give Resultant Force too the Object
                Object.Update(_TimerInterval);

            }
        }
    }
}
