using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
{
    public class InterstellaObjectParams
    {
        Vector _Position;
        Vector _Velocity;
        Vector _Acceleration;

        Vector _Force;

        ScientificNotationValue _Mass;
        double _Density;

        InterstellaObjectType _Type;

        /// <summary>
        /// Constructor to initiate a <see cref="InterstellaObjectParams"/>,  used to create an interstella object, defining Vector properties with doubles 
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="xVelocity"></param>
        /// <param name="yVelocity"></param>
        /// <param name="xAcceleration"></param>
        /// <param name="yAcceleration"></param>
        /// <param name="type"></param>
        /// <param name="mass"></param>
        /// <param name="density"></param>
        public InterstellaObjectParams
            (
                double xPosition,
                double yPosition,
                double xVelocity,
                double yVelocity,
                double xAcceleration,
                double yAcceleration,
                InterstellaObjectType type,
                ScientificNotationValue mass = null,
                double density = double.NaN
            ) :
            this
            (
                new Vector(xPosition, yPosition),
                new Vector(xVelocity, yVelocity),
                new Vector(xAcceleration, yAcceleration),
                type,
                mass,
                density
            )
        { }

        /// <summary>
        /// Constructor for initating a <see cref="InterstellaObjectParams"/> used to create an interstella object
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="acceleration"></param>
        /// <param name="type"></param>
        /// <param name="mass"></param>
        /// <param name="density"></param>
        public InterstellaObjectParams
            (
                Vector position,
                Vector velocity,
                Vector acceleration, 
                InterstellaObjectType type, 
                ScientificNotationValue mass = null, 
                double density = double.NaN
            )
        {
            // Some challange with implimenting this due to as casting not taking a variable containing the type, might need generics
            // Also potentially impliment this method by looking thrpugh keys of params rather to avoid typed errors or using an enumerator and optional flags

            // Foreach Property 
            //  If Property is Null
            //      Set Property to get default of property name as propety.gettype
        
            // Set Density and Mass
            if (mass == null)
            {
                mass = (ScientificNotationValue)InterstellaObjectTypeDefaults.getDefaults(type)["mass"];
            }
            
            if (double.IsNaN(density))
            {
                density = (double)InterstellaObjectTypeDefaults.getDefaults(type)["density"];
            }


            _Position = position;
            _Velocity = velocity;
            _Acceleration = acceleration;

            _Force = new Vector(acceleration * mass.ToDouble());

            _Mass = mass;
            _Density = density;

            _Type = type; 

        }

        public Vector Position { get => _Position; set => _Position = value; }
        public Vector Velocity { get => _Velocity; set => _Velocity = value; }
        public Vector Acceleration { get => _Acceleration; set => _Acceleration = value; }
        public Vector Force { get => _Force; set => _Force = value; }
        public ScientificNotationValue Mass { get => _Mass; set => _Mass = value; }
        public double Density { get => _Density; set => _Density = value; }
        public InterstellaObjectType Type { get => _Type; set => _Type = value; }
    }
}
