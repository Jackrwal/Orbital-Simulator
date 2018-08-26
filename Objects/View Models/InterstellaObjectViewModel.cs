using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OrbitalSimulator_Objects
{
    public class InterstellaObjectViewModel : BaseViewModel
    {
        static ObservableCollection<InterstellaObjectViewModel> InterstellaObjectViewModels = new ObservableCollection<InterstellaObjectViewModel>();

        static ScientificNotationValue _Scale = new ScientificNotationValue(4, -6);

        InterstellaObject _ModelObjectCast { get { return (InterstellaObject)_ModelObject; } }

        double _Width;

        double _Height;

        public double _Radius
        {
            get => _ModelObjectCast.Radius;
            set
            {   // !! These Updates Have to be triggered when _ModelObject.Radius is set,
                //    This can now be done through Property Changed Updates
                _Width  = (_ModelObjectCast.Radius * _Scale.ToDouble()) * 2;
                _Height = (_ModelObjectCast.Radius * _Scale.ToDouble()) * 2;
            }
        }

        public InterstellaObjectViewModel(InterstellaObject interstellaObject)
        {
            base._ModelObject = interstellaObject;

            _Width = (_ModelObjectCast.Radius * _Scale.ToDouble()) * 2;
            _Height = (_ModelObjectCast.Radius * _Scale.ToDouble()) * 2;

            InterstellaObjectViewModels.Add(this);
        }

        public Vector Position
        {
            get => _ModelObjectCast.Position;
            set
            {
                if (_ModelObjectCast.Position == value) return;
                _ModelObjectCast.Position = value;
                NotifyPropertyChanged(this, nameof(Position));
            } 
        }

        public InterstellaObjectType Type { get { return _ModelObjectCast.Type; } }
        public double Width  { get => _Width;  }
        public double Height { get => _Height; }
    }
}
