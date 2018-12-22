using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class InterstellaObjectViewModel : NotifyingViewModel
    {
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
            _InterstellaObject.ViewModel = this;
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

            NotifyPropertyChanged(this, nameof(ScrPositionString));
            NotifyPropertyChanged(this, nameof(VelocityString));
            NotifyPropertyChanged(this, nameof(AccelerationString));
            NotifyPropertyChanged(this, nameof(ForceString));
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

        public double X { get => CanvasPageViewModel.SeperationScaler(_InterstellaObject.Position.X) - Width/2; }
    
        public double Y { get => CanvasPageViewModel.SeperationScaler(_InterstellaObject.Position.Y) - Height/2; }

        public double Width { get => CanvasPageViewModel.RadiusScale(_InterstellaObject.Radius) * 2; }

        public double Height { get => CanvasPageViewModel.RadiusScale(_InterstellaObject.Radius) * 2; }

        public InterstellaObjectType Type { get => _InterstellaObject.Type; }

        public InterstellaObject InterstellaObject { get => _InterstellaObject; set => _InterstellaObject = value; }

        //Displayable Info Strings
        public string ScrPositionString
        {
            get { return $"Position: {Math.Round(X, 1)}, {Math.Round(Y, 1)}"; }
        }
        public string VelocityString
        {
            get { return $"Velocity: {Math.Round(InterstellaObject.Velocity.X, 1)}, {Math.Round(InterstellaObject.Velocity.Y, 1)}"; }
        }
        public string AccelerationString
        {       
            get { return $"Acceleration: {InterstellaObject.Accelleration.X}, {InterstellaObject.Accelleration.Y}"; }
        }
        public string ForceString
        {
            get { return $"Force: {Math.Truncate(InterstellaObject.ResultantForce.X)}, {Math.Truncate(InterstellaObject.ResultantForce.Y)}"; }
        }
    }
}
 