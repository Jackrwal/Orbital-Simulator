using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ## Updates in acceleration must by synchronised with ticks, other wise acceleration is not constant
//    and SUVAT would not work (Would need calculus for variable accerlation which requires functions)

// ## Remove Mass from contructor arguments, set mass based on InterstellaObjectType and update radius accordingly

namespace OrbitalSimulator_Objects
{
    public class InterstellaObject : BaseMovingObject
    {
        private Vector _Momentum;
        private Vector _ResultantForce;
        private double _Mass;
        private double _Radius;
        private InterstellaObjectType _Type;

        public Vector Momentum { get => _Momentum;}
        public Vector ResultantForce { get => _ResultantForce; set => _ResultantForce = value; }
        public double Radius { get => _Radius;}
        public InterstellaObjectType Type { get => _Type; }

        public double Mass
        {
            get => _Mass;
            set { _Mass = value; } // ## Set Radius According to Mass and Scale
       }

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
            // ## Should Acceleration Default to 0?
            _Mass = mass;
            _ResultantForce = new Vector(xForce, yForce);
        }
        #endregion

        // Apply Force too move
    }
}
