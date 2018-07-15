using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ## Updates in acceleration must by synchronised with ticks, other wise acceleration is not constant
//    and SUVAT would not work (Would need calculus for variable accerlation which requires functions)

namespace OrbitalSimulator_Objects
{
    public class InterstellaObject : BaseMovingObject
    {
        public Vector _Momentum { get; }
        public Vector _ResultantForce { get; set; }
        public double _Mass { get; set; }
        public InterstellaObjectType Type { get; }

        #region Constructor

        //Constructor Taking Vector values
        public InterstellaObject(
            double mass, 
            Vector position, 
            Vector velocity, 
            Vector acceleration
            )
            : base(
                  position,
                  velocity,
                  acceleration
                  )
        {

            _Mass = mass;
            _Momentum = Velocity * _Mass;
        }

        // Constructor Taking double values
        public InterstellaObject(
            double mass, 
            double xPosition, 
            double yPosition, 
            double xVeolocity, 
            double yVelocity, 
            double xAcceleration, 
            double yAcceleration
            )
            : this(
                mass, 
                new Vector(xPosition, yPosition),
                new Vector(xVeolocity, yVelocity),
                new Vector(xAcceleration, yAcceleration)
                  )
        {

            _Mass = mass;
        }

        //Consturctor to start with only a position and resultant force
        public InterstellaObject(
            double mass, 
            double xPosition, 
            double yPosition, 
            double xForce, 
            double yForce
            )
            : this(
                mass,
                new Vector(xPosition, yPosition),
                new Vector(0,0), 
                new Vector(xForce / mass, yForce / mass)
                  )
        {

            _Mass = mass;
            _ResultantForce = new Vector(xForce, yForce);
        }
        #endregion

        // Apply Force too move
    }
}
