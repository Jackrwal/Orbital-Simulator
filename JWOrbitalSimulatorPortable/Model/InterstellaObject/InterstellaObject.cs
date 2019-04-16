using System;

namespace JWOrbitalSimulatorPortable.Model
{
    /// <summary>
    /// A Moving Object with the required properties to resolve its excerted gravivational force
    /// and a type and radius used to catagorise and display the object.
    /// </summary>
    public class InterstellaObject : MovingObject
    {
        private double _Mass, _Radius;
        private Vector _ResultantForce;
        private InterstellaObjectType _Type;

        /// <summary>
        /// Constructor to create a new intance of an interstella Object from a InterstellaObject Paramater object
        /// </summary>
        /// <param name="paramaters"><see cref="InterstellaObjectParams"/></param>
        public InterstellaObject(InterstellaObjectParams paramaters) : base(paramaters.Position, paramaters.Velocity, paramaters.Acceleration)
        {
            // Params for MovingObject constructor given to base.
            // Resultant force starts at 0
            _ResultantForce = new Vector(0, 0);

            // Fields set from paramaters
            _Type = paramaters.Type;
            _Mass = paramaters.Mass;
            _Radius = paramaters.Radius;
        }

        /// <summary>
        /// A Constuctor for creating a copy of an existing instance of an InterstellaObject
        /// /// Base Moving object given the paramaters for it's constructor
        /// </summary>
        public InterstellaObject(InterstellaObject copyObject) : base(copyObject.Position, copyObject.Velocity, copyObject.Acceleration)
        {
            _ResultantForce = new Vector(0, 0);

            _Type = copyObject.Type;
            _Mass = copyObject.Mass;
            _Radius = copyObject.Radius;
        }

        public double Mass { get => _Mass; set { _Mass = value; } }

        // Density property given by;
        // D = 3M/4*Pi*r^3
        public double Density { get => (3*Mass/4*Math.PI*Math.Pow(_Radius,3)); }

        public InterstellaObjectType Type { get => _Type; set { _Type = value; } }

        public double Radius { get => _Radius; set { _Radius = value; } }

        public Vector ResultantForce { get => _ResultantForce; set => _ResultantForce = value; }

        /// <summary>
        /// Update moves the base moving object with time elapsed in seconds
        /// and a given acceleraiton.
        /// </summary>
        public void Update(double elapsedMilliseconds, Vector acceleration = null)
        {
            // Acceleration can be passed to the base moving object as null.
            move(elapsedMilliseconds / 1000, acceleration);
        }
    }
}