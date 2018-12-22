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
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class InterstellaObject : MovingObject
    {
        private double _Mass, _Density, _Radius, _SignificantRadius;
        private Vector _ResultantForce;
        private InterstellaObjectType _Type;
        //private List<Point> _TrailPoints = new List<Point>();

        public double Mass { get => _Mass; set { _Mass = value; NotifyPropertyChanged(this, nameof(Mass)); } }

        public double Density { get => _Density; set { _Density = value; NotifyPropertyChanged(this, nameof(Density)); } }

        public InterstellaObjectType Type { get => _Type; set { _Type = value; NotifyPropertyChanged(this, nameof(Type)); } }

        // Too Set Radius, Write a setter too Update Mass and Density from the new Radius value
        public double Radius { get => Math.Pow((3 * _Mass / _Density) / (4 * Math.PI), (1.0 / 3.0)); }

        public double SignificantRadius { get => _SignificantRadius; set => _SignificantRadius = value; }
        public Vector ResultantForce { get => _ResultantForce; set => _ResultantForce = value; }

        /// <summary>
        /// Constructor to create a new intance of an interstella Object from a InterstellaObject Paramater instance
        /// </summary>
        /// <param name="paramaters"></param>
        public InterstellaObject(InterstellaObjectParams paramaters) : base(paramaters.Position, paramaters.Velocity, paramaters.Acceleration)
        {
            _ResultantForce = new Vector(0, 0);

            _Type = paramaters.Type;
            _Mass = paramaters.Mass;
            _Density = paramaters.Density;
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
            _Density = copyObject.Density;
        }

    }
}
