using JWOrbitalSimulatorPortable.Model;
using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vector = JWOrbitalSimulatorPortable.Model.Vector;
using Point = System.Windows.Point;
using System.Diagnostics;

// ## Move As Much of the logic out of here into view models using Relay Commands when convenient.

namespace OrbitalSimulator.Pages
{
    /// <summary>
    /// Interaction logic for CanvasPage.xaml
    /// </summary>
    public partial class CanvasPage : AbstractMVVMPage<CanvasPageViewModel>
    {
        private bool _DragActive;
        private InterstellaObjectViewModel _DraggedObject;

        private double _PanAmount;
        private Key[] _PanKeys = new Key[] { Key.W, Key.A, Key.S, Key.D, Key.Up, Key.Down, Key.Left, Key.Right };
        private List<Key> _KeysDown = new List<Key>();
        private Point _MouseDownPoint;
        private Point _MouseReleasePoint;

        // ## FIX WHEN FILE LOADING Curently Loading Test Sytem in CPVM remove this later to add objects via the UI or file load 
        public CanvasPage() : base(new CanvasPageViewModel(true)) => InitializeComponent();

        // ------------------------------------------------------------------------------------ Drag Drop Handler ----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// On Drop event on canvas add new dragged item to the canvas at drop point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dropNewObject(object sender, DragEventArgs e)
        {
            InterstellaObject DroppedObject = e.Data.GetData(typeof(InterstellaObject)) as InterstellaObject;
            if (DroppedObject == null) return;

            Point DropPoint = e.GetPosition((Canvas)sender);
            Vector DropPositionVector = new Vector(DropPoint.X, DropPoint.Y);

            // It is Important that these are done in this order!
            DropPositionVector = CanvasHelpers.Centrlize(DropPositionVector, CanvasHelpers.CanvasOrigin.TopLeft);
            DropPositionVector -= CanvasPageViewModel.PanVector;
            DropPositionVector = CanvasPageViewModel.InverseSeperationScaler(DropPositionVector);
            DropPositionVector += CanvasPageViewModel.RadiusScale(DroppedObject.Radius);
            
            // ~~ Allow Velocity to be decided on drop, either through drag or relative to another plannet.
            // TODO:
            // Pause model 
            // Make sure object is added
            // If system is played object proceeds with no velocity
            // if the object is then clicked on, then the mouse is dragged to a release point add corrisponding velocity
            // This can be done on any paused object regardless of whether it is new or edited(I dont like that but it is simple and can be changed later).

            DroppedObject.Position = DropPositionVector;

            _VM.AddObject(new InterstellaObject(DroppedObject));
        }

        // ------------------------------------------------------------------------------------ Mouse Events Handlers ----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// When an object is right clicked allow the user to edit the infomation of that object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditObjectOnRightClick(object sender, MouseButtonEventArgs e)
        {
            InterstellaObjectViewModel SenderAsVm = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;

            // ## Do this in the ICommand?
            //Pause System
            CanvasPageViewModel.Instance.SystemRunning = false;

            // Open the data entry box with the sending object's VM
            CanvasPageViewModel.Instance.OpenDataEntryBox.Execute(SenderAsVm);
        }

        /// <summary>
        /// IF SHIFT: When an Object is Left-Clicked focus on this object, by putting it at the centre cavnas
        /// ELSE: start a drag to add velocity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectLeftClicked(object sender, MouseButtonEventArgs e)
        {
            if (_KeysDown.Contains(Key.LeftShift))
            {
                InterstellaObjectViewModel ObjectVm = (InterstellaObjectViewModel)((Ellipse)sender).DataContext;
                CanvasPageViewModel.FocusOnObject(ObjectVm);
            }
            else
            {
                // Only do this if paused?
                _DragActive = true;
                _DraggedObject = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;
            }
        }

        /// <summary>
        /// Record the point on the page at which the mouse is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPage_MouseDown(object sender, MouseButtonEventArgs e)
            => _MouseDownPoint = e.GetPosition((ItemsControl)sender);

        /// <summary>
        /// When the mouse is released over the page, record the point of release and if a drag is active change the velocity of the drag object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasPage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _MouseReleasePoint = e.GetPosition((ItemsControl)sender);

            if (_DragActive)
            {
                // ~~ Change the drag object's velocity proportional to the distance between mouse up and mouse down
                // TODO:
                // Find the difference between mouserelease point and mousedown point
                // Apply a velocity proportionally, in the correct direction (probably will require centering the mouse down and release point)

                Vector CentralReleasePoint = CanvasHelpers.Centrlize(new Vector(_MouseReleasePoint.X, _MouseReleasePoint.Y), CanvasHelpers.CanvasOrigin.TopLeft);
                Vector CentralDownPoint = CanvasHelpers.Centrlize(new Vector(_MouseDownPoint.X, _MouseDownPoint.Y), CanvasHelpers.CanvasOrigin.TopLeft);

                Vector DownReleaseDifference = CentralReleasePoint - CentralDownPoint;

                _DraggedObject.SetObjectVelocity(DownReleaseDifference * 2E2);

                _DragActive = false;
            }
        }

        /// <summary>
        /// On Mouse Up over object if a new object has been dropped put this into orbit around this object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OjectMouseUp(object sender, MouseButtonEventArgs e)
        {
            InterstellaObjectViewModel SenderVM = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;

            if (_DragActive && _DraggedObject != SenderVM)
            { 
                // !! If you click on an object then release over the same object all properties get set to NaN of all objects
                Vector OrbitVelocity = CanvasPageViewModel.GetOrbitVelocity(_DraggedObject.InterstellaObject, SenderVM.InterstellaObject);

                _DraggedObject.SetObjectVelocity(OrbitVelocity);
                _DragActive = false;
            }

        }

        // On Mouse Wheel Zoom about the virtual origin
        private void ZoomOnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double ZoomIncr = ((double)e.Delta) / 1000;

            // Check Zoom Incriment leaves zoom with in a suitable range
            if ((CanvasPageViewModel.MasterScale + ZoomIncr) <= 10 && (CanvasPageViewModel.MasterScale + ZoomIncr) >= 0.1)
                CanvasPageViewModel.MasterScale += ZoomIncr;
        }

        // ------------------------------------------------------------------------------------ Key Events Handlers ----------------------------------------------------------------------------------------------------------

        private void CanvasPage_KeyDown(object sender, KeyEventArgs e)
        {
            if(!e.IsRepeat) _KeysDown.Add(e.Key);

            CanvasPageViewModel DataContextViewModel = (sender as CanvasPage).DataContext as CanvasPageViewModel;

            // !! For Some reason the WD and SD pan combnations breaks panning.

            // If the Key Down is a Pan Key.
            if (_PanKeys.Contains(e.Key))
            {
                if (!e.IsRepeat) _PanAmount = 1;
                else if (_PanAmount < 25) _PanAmount += 1;

                Vector panVector = new Vector(0, 0);

                foreach (var Key in _KeysDown)
                {
                    if (Key == Key.W || Key == Key.Up) panVector.Y += _PanAmount;

                    if (Key == Key.A || Key == Key.Left) panVector.X -= _PanAmount;

                    if (Key == Key.S || Key == Key.Down) panVector.Y -= _PanAmount;

                    if (Key == Key.D || Key == Key.Left) panVector.X += _PanAmount;
                }

                CanvasPageViewModel.Pan(panVector);
            }

            // Switch for other control keys
            switch (e.Key)
            {
                // On Either Plus key, incriment system speed 
                case Key.OemPlus:
                case Key.Add:
                    
                    if (DataContextViewModel.SystemSpeed + 0.1 >= 0.1 && DataContextViewModel.SystemSpeed + 0.1 <= 10)
                        DataContextViewModel.SystemSpeed += 0.1;

                    break;

                // On Either Minus key, decriment system speed 
                case Key.OemMinus:
                case Key.Subtract:

                    if (DataContextViewModel.SystemSpeed - 0.1 >= 0.1 && DataContextViewModel.SystemSpeed - 0.1 <= 10)
                        DataContextViewModel.SystemSpeed -= 0.1;

                    break;

                // Toggle System running on P
                case Key.P:

                    if (!e.IsRepeat && DataContextViewModel.SystemRunning) DataContextViewModel.SystemRunning = false;
                    else if(!e.IsRepeat) DataContextViewModel.SystemRunning = true;

                    // Close Data Entry Box when play, regardless of data altered or not.
                    if (CanvasPageViewModel.Instance.DataBoxVisible)
                        CanvasPageViewModel.Instance.HideDataEntryBox.Execute(null);

                    break;
            }
        }

        private void CanvasPage_KeyUp(object sender, KeyEventArgs e) => _KeysDown.Remove(e.Key);




        // Considering re-implimenting this in some form, not sure if this previously messed with mouse down on individual ellpises on the canvas
        [Obsolete]
        private void FocusOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Clicked Point Relative to the top-left canvas corner
            Point MousePoint = e.GetPosition((ItemsControl)sender);

            // Clicked Point Relative to 0,0
            Vector RealPosition = CanvasHelpers.Centrlize(new Vector(MousePoint.X, MousePoint.Y), CanvasHelpers.CanvasOrigin.TopLeft);

            CanvasPageViewModel.FocusOnPoint(new Vector(RealPosition.X, RealPosition.Y));
        }
    }
}
