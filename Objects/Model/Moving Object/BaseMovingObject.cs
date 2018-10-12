using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Property VarName
//Field   _VarName
//Param    varName & local

namespace OrbitalSimulator_Objects
{
    public abstract class BaseMovingObject : BaseModelObject
    {
        //Static Field Definitions
        static List<BaseMovingObject> _MovingObjects = new List<BaseMovingObject>();
        static Stopwatch _TimeElapsedSinceLastTick = new Stopwatch();

        //static encapsulations
        //static Local Methods

        //Static Public Methods
        static public void Tick()
        {
            foreach (BaseMovingObject MovingObject in _MovingObjects)
            {
                //Pass time elapsed since last tick in milliseconds into the ontick function
                MovingObject.onTick(_TimeElapsedSinceLastTick.ElapsedMilliseconds);
            }
            _TimeElapsedSinceLastTick.Reset();
            _TimeElapsedSinceLastTick.Start();
        }

        //Field Definitions
        Vector _Pos;
        Vector _Velo;
        Vector _Acc;

        public BaseMovingObject(Vector pos, Vector velo, Vector acc)
        {
            _Pos = pos;
            _Velo = velo;
            _Acc = acc;
            _TimeElapsedSinceLastTick.Start();
            _MovingObjects.Add(this);
        }

        //Encapsulations
        public Vector Position { get => _Pos; set { _Pos = value; NotifyPropertyChanged(this, nameof(Position)); } }
        public Vector Velocity { get => _Velo; set { _Velo = value; NotifyPropertyChanged(this, nameof(Velocity)); } }
        public Vector Accelleration { get => _Acc; set { _Acc = value; NotifyPropertyChanged(this, nameof(Accelleration)); } }

        //Public Methods

        //Local Methods
        void onTick(double msElapsedSinceLastTick)
        {
            //Pass the seconds elapsed since last tick into the objects move function
            //move(1);
            move(
                msElapsedSinceLastTick / 1000,
                new Vector(0,0),
                _Velo,
                new Vector(0,0),
                _Acc
            );
        }

        void move(double timeElapsed, Vector Displacement, Vector InitalVelocity, Vector FinalVelocity, Vector Acceleration)
        {
            //SUVAT
            // Displacment;
            // InitalVelocity = _Velo;
            // FinalVecoity;
            // Acceleration = _Acc;
            //T = TimeElasped

            //UPDATE VELOCITY
            //v = u + at
            // If this method is called each tick time will be 1 game tick.
            FinalVelocity = InitalVelocity + (Acceleration * timeElapsed);
            _Velo = FinalVelocity;

            //Displacment
            Displacement = ((InitalVelocity + FinalVelocity) * timeElapsed) / 2;
            //1/2(u+v)t

            //Update Position
            _Pos = Position + Displacement;
        }

        void Accelerate(double xAcceleration, double yAcceleration) { throw new NotImplementedException(); }
        void Accelerate(double resultantAccleration) { throw new NotImplementedException(); }

    }
}
