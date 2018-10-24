using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OrbitalSimulator_Objects
{
    public class InterstellaObjectViewModel : BaseViewModel
    {
        static ScientificNotationValue _BaseScale = new ScientificNotationValue(2, -7);
        static double _MasterScale = 1;

        InterstellaObject _InterstellaObject;

        double _Width;

        double _Height;

        public double _Radius
        {
            get => _InterstellaObject.Radius;
            set
            { 
                // Using Logarithmic scale too reduce range of values
                _Width  = scale(_InterstellaObject.Radius) * 2;
                _Height = scale(_InterstellaObject.Radius) * 2;
            }
        }

        public InterstellaObjectViewModel(InterstellaObject interstellaObject)
        {
            _InterstellaObject = interstellaObject;

            _Width  = scale(_InterstellaObject.Radius) * 2;
            _Height = scale(_InterstellaObject.Radius) * 2;
        }

        public double X
        {
            get => _InterstellaObject.Position.X;
            set
            {
                _InterstellaObject.Position.X = value;

                // !! This Notify Property Changed needs calling when _InterstellaObject.Position.X changes
                NotifyPropertyChanged(this, nameof(X));
            }
        }

        public double Y
        {
            get => _InterstellaObject.Position.Y;
            set
            {
                _InterstellaObject.Position.Y = value;

                // !! This Notify Property Changed needs calling when _InterstellaObject.Position.Y changes
                NotifyPropertyChanged(this, nameof(Y));
            }
        }

        public InterstellaObjectType Type { get { return _InterstellaObject.Type; } }

        public double Width  { get => _Width;  }
        public double Height { get => _Height; }

        // s(R) = 60kLog(sR+1)
        // s(R) = Scaled Radius where s is scaler function
        // R is real Radius
        // s is BaseScaler
        // k is MasterScaled
        /// <summary>
        /// Private Helper Function to scale the Real Radius of a planet too a size that can be displayed
        /// A base scale factor is used to reudce the radius of a suitable size
        /// A Logarithmic scale is used to reduce the range in size of plannets
        /// A Master Scaler is then applied to control the scope of the system
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        private double scale(double radius) => 60 * _MasterScale * Math.Log10((_BaseScale.ToDouble() * radius) + 1);

    }
}
