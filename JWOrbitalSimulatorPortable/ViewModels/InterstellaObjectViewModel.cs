using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.ObjectModel;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// View Model to present an interstellar object to the view.
    /// </summary>
    public class InterstellaObjectViewModel : NotifyingViewModel
    {
        // The Instance of an interstellar object this view model wraps
        private InterstellaObject _InterstellaObject;

        /// <summary>
        /// Construct Object ViewModel as a wrap around a <see cref="InterstellaObject"/> to present it to a CanvasPage
        /// </summary>
        /// <param name="interstellaObject"></param>
        public InterstellaObjectViewModel(InterstellaObject interstellaObject)
        {
            _InterstellaObject = interstellaObject;

            // Subcribe to the Interstellar Object's updated event
            _InterstellaObject.ObjectUpdated += interstellaObject_ObjectUpdated;
        }

        /// <summary>
        /// The scaled logical position of this item.
        /// </summary>
        public Vector Position => CanvasPageViewModel.SeperationScaler(_InterstellaObject.Position);

        /// <summary>
        /// The Screen Position of the object relative to the Top-left canvas origin
        /// </summary>
        public Vector ScreenPosition => getScreenXY();

        // Width and Height of the scaled object
        public double Width { get => CanvasPageViewModel.RadiusScale(_InterstellaObject.Radius) * 2;  }
        public double Height { get => CanvasPageViewModel.RadiusScale(_InterstellaObject.Radius) * 2; }

        // Radius and Mass used to alter object Mass and Radius from the UI.
        public double Radius { get { return InterstellaObject.Radius; } set { InterstellaObject.Radius = value; } }
        public double Mass { get { return InterstellaObject.Mass; } set { InterstellaObject.Mass = value; } }

        // The type of this object. Used to display it to the UI
        public InterstellaObjectType Type { get => _InterstellaObject.Type; }

        // used to access the object for changing velocity on drag.
        public InterstellaObject InterstellaObject { get => _InterstellaObject; set => _InterstellaObject = value; }

        // The Collection of previous position points making up the object's trail
        public ObservableCollection<Vector> TrailPoints { get; set; } = new ObservableCollection<Vector>();
        public string TrailColour { get; set; } = "#FFFFFFFF";

        // The Maxiumum number of previous positions to store.
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
        // Property Strings used to present infomation about the object to the user in InfoPannel
        public string VelocityString
        {
            get { return $"Velocity: {InterstellaObject.Velocity.X.Round(4)}, {InterstellaObject.Velocity.Y.Round(4)}"; }
        }
        public string AccelerationString
        {       
            get { return $"Acceleration: {InterstellaObject.Acceleration.X.Round(4)}, {InterstellaObject.Acceleration.Y.Round(4)}"; }
        }
        public string ForceString
        {
            get { return $"Force: {InterstellaObject.ResultantForce.X.Round(4)}, {InterstellaObject.ResultantForce.Y.Round(4)}"; }
        }

        /// <summary>
        /// Set the model object's velocity to a given vector
        /// </summary>
        public void SetObjectVelocity(Vector newVelocity)
        {
            InterstellaObject.Velocity = newVelocity;
        }
        
        /// <summary>
        /// Get the scaled position of the top-left corner of the object relative to the top-left of the canvas.
        /// </summary>
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

            // Width and Height may have changed due to change in radius scaling constants
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

        //  Due to technical issues displaying trails in the UI and time constraints the trails are not enabled.
        /// <summary>
        /// Update the bodies trail by removing its oldest point if its at its trail length then addings its new position
        /// </summary>
        private void updateTrail()
        {
            if (TrailPoints.Count >= TrailLength)
            {
                TrailPoints.RemoveAt(0);
            }

            TrailPoints.Add(new Vector(ScreenPosition.X, ScreenPosition.Y));
        }
    }
}