using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class InterstellaObjectViewModel : NotifyingViewModel
    {
        static double _BaseScale = 2E-7;
        static double _MasterScale = 1;

        private InterstellaObject _InterstellaObject;

        /// <summary>
        /// Construct Object ViewModel as a wrap around a <see cref="InterstellaObject"/> to present it to a CanvasPage
        /// </summary>
        /// <param name="interstellaObject"></param>
        public InterstellaObjectViewModel(InterstellaObject interstellaObject)
        {
            _InterstellaObject = interstellaObject;
            _InterstellaObject.PropertyChanged += _InterstellaObject_PropertyChanged;
            _InterstellaObject.ObjectUpdated += _InterstellaObject_ObjectUpdated;
        }

        /// <summary>
        /// When the Model Object Has been updated notify the View that its Location may have changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _InterstellaObject_ObjectUpdated(object sender, EventArgs e)
        {
            NotifyPropertyChanged(this, nameof(X));
            NotifyPropertyChanged(this, nameof(Y));
        }

        /// <summary>
        /// When a property Changes in the model Notify the View of this change so it can call the Getter of this property returning its new value to the view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _InterstellaObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_InterstellaObject.Radius))
            {
                NotifyPropertyChanged(this, nameof(Width));
                NotifyPropertyChanged(this, nameof(Height));
                return;
            }
            if (e.PropertyName == nameof(_InterstellaObject.Type))
            {
                NotifyPropertyChanged(this, nameof(Type));
                return;
            }

            NotifyPropertyChanged(this, e.PropertyName);
        }
        
        private double scale(double radius) => 60 * _MasterScale * Math.Log10((_BaseScale * radius) + 1);

        public double X  { get => _InterstellaObject.Position.X; }

        public double Y { get => _InterstellaObject.Position.Y; }

        public double Width { get => scale(_InterstellaObject.Radius) * 2; }

        public double Height { get => scale(_InterstellaObject.Radius) * 2; }

        public InterstellaObjectType Type { get => _InterstellaObject.Type; }
    }
}
