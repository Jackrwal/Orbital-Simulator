using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Property VarName
//Field   _VarName
//Param    varName

namespace OrbitalSimulator_Objects
{
    public abstract class BaseMovingObject
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
                // !! Elasped Milliseconds appears as 0
            }
            _TimeElapsedSinceLastTick.Reset();
            _TimeElapsedSinceLastTick.Start();
        }

        //Field Definitions
        Vector _Pos;
        Vector _Velo;
        Vector _Acc;

        public BaseMovingObject(Vector Pos, Vector Velo, Vector Acc)
        {
            _Pos = Pos;
            _Velo = Velo;
            _Acc = Acc;
            _TimeElapsedSinceLastTick.Start();
            _MovingObjects.Add(this);
        }

        //Encapsulations
        public Vector Position { get => _Pos; set => _Pos = value; }
        public Vector Velocity { get => _Velo; set => _Velo = value; }
        public Vector Accelleration { get => _Acc; set => _Acc = value; }

        //Public Methods

        //Local Methods
        void onTick(double msElapsedSinceLastTick)
        {
            //Pass the seconds elapsed since last tick into the objects move function
            move(msElapsedSinceLastTick/1000);
            //move(1);
        }

        void move(double timeElapsed)
        {
            //SUVAT
            Vector Displacment;
            Vector InitalVelocity = _Velo;
            Vector FinalVecoity;
            Vector Acceleration = _Acc;
            //T = TimeElasped

            //UPDATE VELOCITY
            //v = u + at
            // If this method is called each tick time will be 1 game tick.
            FinalVecoity = InitalVelocity + (Acceleration * timeElapsed);
            _Velo = FinalVecoity;

            //Displacment
            Displacment = ((InitalVelocity + FinalVecoity) * timeElapsed) / 2;
            //1/2(u+v)t
            
            //Update Position
            _Pos = Position + Displacment;
        }
        
        void Accelerate(double XAcceleration, double YAcceleration) { }
        void Accelerate(double ResultantAccleration) { }

    }
}
