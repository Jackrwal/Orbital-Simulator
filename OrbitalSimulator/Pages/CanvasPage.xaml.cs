using JWOrbitalSimulatorPortable.Model;
using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Vector = JWOrbitalSimulatorPortable.Model.Vector;
using Point = System.Windows.Point;

namespace OrbitalSimulator.Pages
{
    /// <summary>
    /// Interaction logic for CanvasPage.xaml
    /// </summary>
    public partial class CanvasPage : AbstractMVVMPage<CanvasPageViewModel>
    {
        // Flags if a drag, to change the velocity of an object, is in process
        // ( the mouse has been put down over an object but not released)
        private bool _DragActive;

        // If a drag is active this is the object over which mouse down occured
        private InterstellaObjectViewModel _DraggedObject;

        // The amount to pan by for each tick that the key is down
        private double _PanAmount;

        // Keys that can be used to pan the system
        private Key[] _PanKeys = new Key[] { Key.W, Key.A, Key.S, Key.D, Key.Up, Key.Down, Key.Left, Key.Right };

        // Keys taht are currently held down
        private List<Key> _KeysDown = new List<Key>();

        // The points at which the mouse down and mouse up events occured
        private Point _MouseDownPoint;
        private Point _MouseReleasePoint;

        // Base constructor creates a new instance of canvaspage view model
        public CanvasPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On Drop event on canvas add new dragged item to the canvas at drop point
        /// </summary>
        private void dropNewObject(object sender, DragEventArgs e)
        {
            // Get the object that was dropped
            InterstellaObject DroppedObject = e.Data.GetData(typeof(InterstellaObject)) as InterstellaObject;

            // If Failed to get object return
            if (DroppedObject == null) return;

            // Get the point at which the object was dropped as a vector
            Point DropPoint = e.GetPosition((Canvas)sender);
            Vector DropPositionVector = new Vector(DropPoint.X, DropPoint.Y);

            // Find and set the relative position in the model of this object
            // It is Important that these are done in this order!
            DropPositionVector = Helpers.Centrlize(DropPositionVector, Helpers.CanvasOrigin.TopLeft);
            DropPositionVector -= CanvasPageViewModel.PanVector;
            DropPositionVector = CanvasPageViewModel.InverseSeperationScaler(DropPositionVector);
            DropPositionVector += CanvasPageViewModel.RadiusScale(DroppedObject.Radius);
            
            DroppedObject.Position = DropPositionVector;

            // Use the View Model to add the object, at this position, to the model
            _VM.AddObject(new InterstellaObject(DroppedObject));
        }

        /// <summary>
        /// When an object is right clicked allow the user to edit the infomation of that object
        /// using the hover data entry box
        /// </summary>
        private void EditObjectOnRightClick(object sender, MouseButtonEventArgs e)
        {
            // Get the view model of the clicked object
            InterstellaObjectViewModel SenderAsVm = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;

            //Pause System
            CanvasPageViewModel.Instance.SystemRunning = false;

            // Open the data entry box with the sending object's VM
            CanvasPageViewModel.Instance.OpenDataEntryBox.Execute(SenderAsVm);
        }

        /// <summary>
        /// When object is left clicked;
        /// IF SHIFT Down: When an Object is Left-Clicked focus on this object, by putting it at the centre cavnas
        /// ELSE: start a drag to add velocity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectLeftClicked(object sender, MouseButtonEventArgs e)
        {
            // If the shift key is currently down
            if (_KeysDown.Contains(Key.LeftShift))
            {
                InterstellaObjectViewModel ObjectVm = (InterstellaObjectViewModel)((Ellipse)sender).DataContext;
                CanvasPageViewModel.FocusOnObject(ObjectVm);
            }
            else
            {
                // flag a drag event as active and set the dragged object to be the one that was clicked
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
            // Get the position at which the house was released relative to the canvas
            _MouseReleasePoint = e.GetPosition((ItemsControl)sender);

            // If a drag drop is in process..
            if (_DragActive)
            {
                // Find the difference between the mouse up and down points relative to the centre of the canvas
                Vector CentralReleasePoint = Helpers.Centrlize(new Vector(_MouseReleasePoint.X, _MouseReleasePoint.Y), Helpers.CanvasOrigin.TopLeft);
                Vector CentralDownPoint = Helpers.Centrlize(new Vector(_MouseDownPoint.X, _MouseDownPoint.Y), Helpers.CanvasOrigin.TopLeft);

                Vector DownReleaseDifference = CentralReleasePoint - CentralDownPoint;

                // And use it to set the object's velocity and end the drag operation
                _DraggedObject.InterstellaObject.Velocity = DownReleaseDifference * 2E2;
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
            // Get the view model of the object that mouse was releasd over
            InterstellaObjectViewModel SenderVM = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;

            // If drag is active, and object overwhich the mouse was released is not the same as the drag object
            if (_DragActive && SenderVM != _DraggedObject)
            { 
                // Calculate the velocity require of the drag object to orbit the other
                Vector OrbitVelocity = CanvasPageViewModel.GetOrbitVelocity(_DraggedObject.InterstellaObject, SenderVM.InterstellaObject);

                //set the object's velocity to the calculated above, and end the drag operation
                _DraggedObject.SetObjectVelocity(OrbitVelocity);
                _DragActive = false;
            }
        }

        /// <summary>
        /// When the mouse wheel is scrolled change the zoom
        /// </summary>
        private void ZoomOnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Use the incriment in zoom as a thousanth of the change on the scroll wheel
            double ZoomIncr = ((double)e.Delta) / 1000;

            // Check Zoom Incriment leaves zoom with in a suitable range before adding to master scale
            if ((CanvasPageViewModel.MasterScale + ZoomIncr) <= 10 && (CanvasPageViewModel.MasterScale + ZoomIncr) >= 0.1)
                CanvasPageViewModel.MasterScale += ZoomIncr;
        }

        /// <summary>
        /// On key down check against key bindings
        /// </summary>
        private void CanvasPage_KeyDown(object sender, KeyEventArgs e)
        {
            // If this is the first tick for which the key is down add it to keys down
            if(!e.IsRepeat)
                _KeysDown.Add(e.Key);

            // get a shortened identifer to the canvas page view model instance
            // (for readability)
            CanvasPageViewModel CPVM = CanvasPageViewModel.Instance;

            // If the Key Down is a Pan Key.
            if (_PanKeys.Contains(e.Key))
            {
                // If key is a repeat, add one to the pan amount
                if (!e.IsRepeat) _PanAmount = 1;
                else if (_PanAmount < 25) _PanAmount += 1;

                // start the pan vector as 0,0
                Vector panVector = new Vector(0, 0);

                // add pan amount for every pan key that is down.
                foreach (var Key in _KeysDown)
                {
                    if (Key == Key.W || Key == Key.Up)
                        panVector.Y += _PanAmount;

                    else if (Key == Key.A || Key == Key.Left)
                        panVector.X -= _PanAmount;

                    else if (Key == Key.S || Key == Key.Down)
                        panVector.Y -= _PanAmount;

                    else if (Key == Key.D || Key == Key.Left)
                        panVector.X += _PanAmount;
                }

                // Pan the system by the pan vector
                CanvasPageViewModel.Pan(panVector);
            }

            // Switch for other control keys
            switch (e.Key)
            {
                // On Either Plus key, incriment system speed 
                case Key.OemPlus:
                case Key.Add:
                    
                    if (CPVM.SystemSpeed + 0.5 >= 0.5 && CPVM.SystemSpeed + 0.5 <= 20)
                        CPVM.SystemSpeed += 0.5;

                    break;

                // On Either Minus key, decriment system speed 
                case Key.OemMinus:
                case Key.Subtract:

                    if (CPVM.SystemSpeed - 0.5 >= 0.5 && CPVM.SystemSpeed - 0.5 <= 20)
                        CPVM.SystemSpeed -= 0.5;

                    break;

                // Toggle System running on P
                case Key.P:

                    if (!e.IsRepeat && CPVM.SystemRunning) CPVM.SystemRunning = false;
                    else if(!e.IsRepeat) CPVM.SystemRunning = true;

                    // Close Data Entry Box when play, regardless of data altered or not.
                    if (CanvasPageViewModel.Instance.DataBoxVisible)
                        CanvasPageViewModel.Instance.HideDataEntryBox.Execute(null);

                    break;

                // Pause, and open the canvas escape menu on escape. Or vise verser for closing.
                case Key.Escape:
                    if(!CanvasPageViewModel.Instance.SystemRunning && CanvasPageViewModel.Instance.EscMenu.Visiblity) CanvasPageViewModel.Instance.Play();
                    else CanvasPageViewModel.Instance.Pause();

                    CanvasPageViewModel.Instance.EscMenu.ToggleEscMenu();
                    break;
            }
        }

        /// <summary>
        /// When a key is released remove all instances of this key from the keys down list
        /// </summary>
        private void CanvasPage_KeyUp(object sender, KeyEventArgs e)
            => _KeysDown.RemoveAll((key) => key == e.Key);
    }
}