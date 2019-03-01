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
        private double _Radius
        {
            get { return _InterstellaObject.Radius; }
        }

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
        /// The scaled logical position of this item.
        /// </summary>
        public Vector Position => CanvasPageViewModel.SeperationScaler(_InterstellaObject.Position);

        /// <summary>
        /// The Screen Position of the object relative to the Top-left canvas origin
        /// </summary>
        public Vector ScreenPosition => getScreenXY();

        // Width and Height Have Private setters for updating values when scaling changes
        public double Width { get => CanvasPageViewModel.RadiusScale(_Radius) * 2;  }

        public double Height { get => CanvasPageViewModel.RadiusScale(_Radius) * 2; }

        public InterstellaObjectType Type { get => _InterstellaObject.Type; }

        // ## I shouldnt be able to access the model directly from the view, sort this out when tidying up
        public InterstellaObject InterstellaObject { get => _InterstellaObject; set => _InterstellaObject = value; }

        public ObservableCollection<Point> TrailPoints { get; set; } = new ObservableCollection<Point>();
        public string TrailColour { get; set; } = "#FFFFFFFF";
        public int TrailLength = 25;

        //Displayable Info Strings
        public string ScrPositionString
        {
            get
            {
                // Give Screen Position Relative too canvas center
                Vector RelativeOrigin = CanvasHelpers.Centrlize(getScreenXY(), CanvasHelpers.CanvasOrigin.TopLeft);
                return $"Position: { (int)RelativeOrigin.X }, { (int)RelativeOrigin.Y }";
            }
        }
        public string VelocityString
        {
            get { return $"Velocity: {InterstellaObject.Velocity.X.Round(4)}, {InterstellaObject.Velocity.Y.Round(4)}"; }
        }
        public string AccelerationString
        {       
            get { return $"Acceleration: {InterstellaObject.Accelleration.X.Round(4)}, {InterstellaObject.Accelleration.Y.Round(4)}"; }
        }
        public string ForceString
        {
            get { return $"Force: {InterstellaObject.ResultantForce.X.Round(4)}, {InterstellaObject.ResultantForce.Y.Round(4)}"; }
        }

        public void SetObjectVelocity(Vector newVelocity)
        {
            InterstellaObject.Velocity = newVelocity;
        }
         
        private Vector getScreenXY()
        {
            Vector ScreenPosition = CanvasPageViewModel.SeperationScaler(_InterstellaObject.Position);
            ScreenPosition += CanvasPageViewModel.PanVector;
            ScreenPosition = CanvasHelpers.PointFromRelativeOrigin(ScreenPosition, CanvasHelpers.CanvasOrigin.TopLeft);
            ScreenPosition -= (Width / 2);

            return ScreenPosition;
        }

        /// <summary>
        /// When the Model Object Has been updated notify the View that its Location may have changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _InterstellaObject_ObjectUpdated(object sender, EventArgs e)
        {

            // Update Object Position
            NotifyPropertyChanged(this, nameof(ScreenPosition));

            // Notify Change in Width & Height incase radius scaling has changed
            // It is not nessasary to notify of W/H changes every update, only when master scale is changed
            // However master scale is a static property making accessing individual VMs to notify of raidus update on its changing is a more effecient but longer solution
            NotifyPropertyChanged(this, nameof(Width));
            NotifyPropertyChanged(this, nameof(Height));

            // Update Object Trail
            updateTrail();
            NotifyPropertyChanged(this, nameof(TrailPoints));

            // Update Object Infomation Strings
            NotifyPropertyChanged(this, nameof(ScrPositionString));
            NotifyPropertyChanged(this, nameof(VelocityString));
            NotifyPropertyChanged(this, nameof(AccelerationString));
            NotifyPropertyChanged(this, nameof(ForceString));
        }


        // ~~ Currently Trails are not Being Placed Correctly SEE CanvasPage.XAML L57
        /// <summary>
        /// Update the bodies trail by removing its oldest point if its at its trail length then addings its new position
        /// </summary>
        private void updateTrail()
        {
            if (TrailPoints.Count >= TrailLength)
            {
                TrailPoints.RemoveAt(0);
            }

            TrailPoints.Add(new Point(ScreenPosition.X, ScreenPosition.Y));
        }


        // ## This function isnt really nessesary i dont think?
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
    }
}
 