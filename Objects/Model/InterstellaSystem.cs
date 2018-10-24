using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace OrbitalSimulator_Objects
{
    public class InterstellaSystem
    {
        private List<InterstellaObject> _InterstellaObjects = new List<InterstellaObject>();
        private int _TickInterval = 100;
        private Timer _SystemClock;

        public InterstellaSystem()
        {

        }

        public InterstellaSystem(List<InterstellaObject> interstellaObjects)
        {
            _InterstellaObjects = interstellaObjects;
        }

        public List<InterstellaObject> InterstellaObjects { get => _InterstellaObjects; set => _InterstellaObjects = value; }

        public void Start()
        {
            _SystemClock = new Timer(new TimerCallback(onClockTick), new ClockState(), 0, _TickInterval);
        }

        public void Start(int interval)
        {
            _TickInterval = interval;
            Start();
        }

        public void Stop()
        {
            _SystemClock.Dispose();
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void SaveSystem()
        {
            throw new NotImplementedException();
        }

        public void LoadSystem()
        {
            throw new NotImplementedException();
        }

        private void onClockTick(object timerState)
        {
            foreach (InterstellaObject Object in _InterstellaObjects)
            {
                Object.Update(_TickInterval);
                //Debug.Print($"{Object.Position.X.ToString()} , {Object.Position.Y.ToString()}");
            }
        }
    }

    class ClockState
    {

    }
}
