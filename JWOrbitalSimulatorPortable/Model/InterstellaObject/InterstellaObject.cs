using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public struct Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }

    public class InterstellaObject : MovingObject
    {
        private double _Mass, _Radius;
        private Vector _ResultantForce;
        private InterstellaObjectType _Type;

        /// <summary>
        /// Constructor to create a new intance of an interstella Object from a InterstellaObject Paramater instance
        /// </summary>
        /// <param name="paramaters"></param>
        public InterstellaObject(InterstellaObjectParams paramaters) : base(paramaters.Position, paramaters.Velocity, paramaters.Acceleration)
        {
            _ResultantForce = new Vector(0, 0);

            _Type = paramaters.Type;
            _Mass = paramaters.Mass;
            _Radius = paramaters.Radius;
        }

        /// <summary>
        /// A Constuctor for creating a copy of an existing instance of an InterstellaObject
        /// </summary>
        /// <param name="copyObject"></param>
        public InterstellaObject(InterstellaObject copyObject) : base(copyObject.Position, copyObject.Velocity, copyObject.Accelleration)
        {
            _ResultantForce = new Vector(0, 0);

            _Type = copyObject.Type;
            _Mass = copyObject.Mass;
            _Radius = copyObject.Radius;
        }

        public double Mass { get => _Mass; set { _Mass = value; NotifyPropertyChanged(this, nameof(Mass)); } }

        // D = 3M/4*Pi*r^3
        public double Density { get => (3*Mass/4*Math.PI*Math.Pow(_Radius,3)); }

        public InterstellaObjectType Type { get => _Type; set { _Type = value; NotifyPropertyChanged(this, nameof(Type)); } }

        // Too Set Radius, Write a setter too Update Mass and Density from the new Radius value
        public double Radius { get => _Radius; set { _Radius = value; NotifyPropertyChanged(this, nameof(Radius)); } }

        public Vector ResultantForce { get => _ResultantForce; set => _ResultantForce = value; }

        public void Update(double elapsedMilliseconds, Vector acceleration = null)
        {
            move(elapsedMilliseconds / 1000, acceleration);
        }
    }
}
