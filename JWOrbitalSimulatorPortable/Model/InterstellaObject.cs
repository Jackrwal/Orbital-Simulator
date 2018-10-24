using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public class InterstellaObject : MovingObject
    {
        private double _Mass, _Density, _Radius, _SignificantRadius;
        private Vector _ResultantForce;
        private InterstellaObjectType _Type;

        public double Mass { get => _Mass; set { _Mass = value; NotifyPropertyChanged(this, nameof(Mass)); UpdateRadius(); } }
        public double Density { get => _Density; set { _Density = value; NotifyPropertyChanged(this, nameof(Density)); UpdateRadius(); } }
        public InterstellaObjectType Type { get => _Type; set { _Type = value; NotifyPropertyChanged(this, nameof(Type)); } }
        public double Radius { get => _Radius; set { _Radius = value; NotifyPropertyChanged(this, nameof(Type)); } }

        public double SignificantRadius { get => _SignificantRadius; set => _SignificantRadius = value; }
        public Vector ResultantForce { get => _ResultantForce; set => _ResultantForce = value; }

        public InterstellaObject(InterstellaObjectParams paramaters) : base(paramaters.Position, paramaters.Velocity, paramaters.Acceleration)
        {
            _Type = paramaters.Type;
            _Mass = paramaters.Mass;
            _Density = paramaters.Density;
            UpdateRadius();
        }

        private void UpdateRadius() => _Radius = Math.Pow((3 * _Mass / _Density) / (4 * Math.PI), (1.0 / 3.0));
    }
}
