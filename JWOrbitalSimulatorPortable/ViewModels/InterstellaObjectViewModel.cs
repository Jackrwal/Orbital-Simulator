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
            _InterstellaObject.ObjectUpdated += interstellaObject_ObjectUpdated;
            _InterstellaObject.ViewModel = this;
            //_InterstellaObject.PropertyChanged += _InterstellaObject_PropertyChanged;
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
        public double Width { get => CanvasPageViewModel.RadiusScale(_InterstellaObject.Radius) * 2;  }

        public double Height { get => CanvasPageViewModel.RadiusScale(_InterstellaObject.Radius) * 2; }

        // Radius and Mass used to alter object Mass and Radius from the UI.
        public double Radius { get { return InterstellaObject.Radius; } set { InterstellaObject.Radius = value; } }

        public double Mass { get { return InterstellaObject.Mass; } set { InterstellaObject.Mass = value; } }

        public InterstellaObjectType Type { get => _InterstellaObject.Type; }

        // ## I shouldnt be able to access the model directly from the view, sort this out when tidying up
        // Used to access the data object for DragDrop.
        public InterstellaObject InterstellaObject { get => _InterstellaObject; set => _InterstellaObject = value; }

        public ObservableCollection<Point> TrailPoints { get; set; } = new ObservableCollection<Point>();
        public string TrailColour { get; set; } = "#FFFFFFFF";
        public int TrailLength = 25;

        //Gives the current position of the object relative to the canvas page's focus.
        public string ScrPositionString
        {
            get
            {
                // Give Screen Position Relative too canvas center
                Vector RelativeOrigin = Helpers.Centrlize(getScreenXY(), Helpers.CanvasOrigin.TopLeft);
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
            ScreenPosition = Helpers.PointFromRelativeOrigin(ScreenPosition, Helpers.CanvasOrigin.TopLeft);
            ScreenPosition -= (Width / 2);

            return ScreenPosition;
        }

        /// <summary>
        /// When the Model Object Has been updated notify the View that properties of the viewmodel may have changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void interstellaObject_ObjectUpdated(object sender, EventArgs e)
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
    }
}
 