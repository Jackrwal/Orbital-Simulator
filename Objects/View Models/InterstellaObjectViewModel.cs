using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OrbitalSimulator_Objects
{
    public class InterstellaObjectViewModel : BaseViewModel
    {

        static ScientificNotationValue _Scale = new ScientificNotationValue(4, -6);

        InterstellaObject _ModelObjectCast { get { return (InterstellaObject)_ModelObject; } }

        double _Width;

        double _Height;

        public double _Radius
        {
            get => _ModelObjectCast.Radius;
            set
            { 
                _Width = (_ModelObjectCast.Radius * _Scale.ToDouble()) * 2;
                _Height = (_ModelObjectCast.Radius * _Scale.ToDouble()) * 2;
            }
        }

        public InterstellaObjectViewModel(InterstellaObject interstellaObject)
        {
            base._ModelObject = interstellaObject;

            _Width = (_ModelObjectCast.Radius * _Scale.ToDouble()) * 2;
            _Height = (_ModelObjectCast.Radius * _Scale.ToDouble()) * 2;

        }

        public double X
        {
            get => _ModelObjectCast.Position.X;
            set
            {
                if (_ModelObjectCast.Position.X == value) return;
                _ModelObjectCast.Position.X = value;
                NotifyPropertyChanged(this, nameof(_ModelObjectCast.Position.X));
            }
        }

        public double Y
        {
            get => _ModelObjectCast.Position.Y;
            set
            {
                if (_ModelObjectCast.Position.Y == value) return;
                _ModelObjectCast.Position.Y = value;
                NotifyPropertyChanged(this, nameof(_ModelObjectCast.Position.Y));
            }
        }

        public InterstellaObjectType Type { get { return _ModelObjectCast.Type; } }
        public double Width  { get => _Width;  }
        public double Height { get => _Height; }
    }
}
