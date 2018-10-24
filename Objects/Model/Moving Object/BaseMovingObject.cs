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
        //Field Definitions
        Vector _Pos;
        Vector _Velo;
        Vector _Acc;

        public BaseMovingObject(Vector pos, Vector velo, Vector acc)
        {
            _Pos = pos;
            _Velo = velo;
            _Acc = acc;
        }

        public Vector Position { get => _Pos; set { _Pos = value; NotifyPropertyChanged(this, nameof(Position)); } }
        public Vector Velocity { get => _Velo; set { _Velo = value; NotifyPropertyChanged(this, nameof(Velocity)); } }
        public Vector Accelleration { get => _Acc; set { _Acc = value; NotifyPropertyChanged(this, nameof(Accelleration)); } }

        //Local Methods
        public void Update(double msElapsedSinceLastUpdate)
        {
            //Pass the seconds elapsed since last tick into the objects move function
            move(
                msElapsedSinceLastUpdate / 1000,
                new Vector(0,0),
                _Velo,
                new Vector(0,0),
                _Acc
            );
        }

        void move(double timeElapsed, Vector displacement, Vector initalVelocity, Vector finalVelocity, Vector acceleration)
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
            finalVelocity = initalVelocity + (acceleration * timeElapsed);
            _Velo = finalVelocity;

            //Displacment
            displacement = ((initalVelocity + finalVelocity) * timeElapsed) / 2;
            //1/2(u+v)t

            //Update Position
            _Pos = Position + displacement;
        }
    }
}