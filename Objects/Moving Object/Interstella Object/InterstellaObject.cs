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

        //kg
        private double _Mass;

        //kg/m^3
        private double _Density;

        private double _Scale;
        private double _Density;
        private double _Radius;
        private InterstellaObjectType _Type;

        public Vector Momentum { get => _Momentum;}
        public Vector ResultantForce { get => _ResultantForce; set => _ResultantForce = value; }
        public double Radius { get => _Radius;}
        public InterstellaObjectType Type { get => _Type; }

        public double Mass
        {
            get => _Mass;
            set { _Mass = value; updateRadius(); } // ## Set Radius According to Mass and Scale
       }

        #region Constructor

        // !! Last thing done was setting default Density (Density of earch) and default scale (Earth 63 pixles radius)
        // but ran into a Framework refference error

        //Constructor Taking Vector values
        public InterstellaObject(
            double mass, 
            double density = 5510,
            double scale = 0.00001,
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
            // ## Update Default Density / Mass based on InterstellaObjectType

            _Mass = mass;
            updateRadius();

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
            UpdateRadius;
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
            UpdateRadius;

            _ResultantForce = new Vector(xForce, yForce);
        }
        #endregion

        // Apply Force too move

        private void updateRadius()
        {
            // Volume = Mass / Denity
            // r = cube root((3V)/4 pi) 

            // !! Set Density and Scale In Ctors

            double volume = _Mass / _Density;
            _Radius = Math.Pow((3 * volume) / (4 * Math.PI), (1 / 3)) * _Scale;
        }

        // Later functions will be needed to update Volume, Mass and density for UI Sliders 
        // To Update Other Values
        // This could be compliacted because of the relation between them, When a user updates Mass should density or volume change?
    }
}
