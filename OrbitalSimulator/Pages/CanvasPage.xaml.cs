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

// ## Given more time i would have Moved much of this functionality into the Canvas Page View Model, and would have seperated the CPVM 
// into canvaspage viewmodel which handles interactions with the page and its controls. And a InterstellarSystemViewModel to handle the scaling and interactions with the logical system

// ## Given More time i would have added an arrow showing the velocity added to a plannet during a drag to add velocity

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

        public CanvasPage() : base(new CanvasPageViewModel())
        {
            InitializeComponent();
        }



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
            DropPositionVector = Helpers.Centrlize(DropPositionVector, Helpers.CanvasOrigin.TopLeft);
            DropPositionVector -= CanvasPageViewModel.PanVector;
            DropPositionVector = CanvasPageViewModel.InverseSeperationScaler(DropPositionVector);
            DropPositionVector += CanvasPageViewModel.RadiusScale(DroppedObject.Radius);
            
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
                Vector CentralReleasePoint = Helpers.Centrlize(new Vector(_MouseReleasePoint.X, _MouseReleasePoint.Y), Helpers.CanvasOrigin.TopLeft);
                Vector CentralDownPoint = Helpers.Centrlize(new Vector(_MouseDownPoint.X, _MouseDownPoint.Y), Helpers.CanvasOrigin.TopLeft);

                Vector DownReleaseDifference = CentralReleasePoint - CentralDownPoint;

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
            InterstellaObjectViewModel SenderVM = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;

            if (_DragActive && SenderVM != _DraggedObject)
            { 
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
            if(!e.IsRepeat)
                _KeysDown.Add(e.Key);

            CanvasPageViewModel CPVM = CanvasPageViewModel.Instance;

            

            // If the Key Down is a Pan Key.
            if (_PanKeys.Contains(e.Key))
            {
                if (!e.IsRepeat) _PanAmount = 1;
                else if (_PanAmount < 25) _PanAmount += 1;

                Vector panVector = new Vector(0, 0);

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

                case Key.Escape:
                    if(!CanvasPageViewModel.Instance.SystemRunning && CanvasPageViewModel.Instance.EscMenu.Visiblity) CanvasPageViewModel.Instance.Play();
                    else CanvasPageViewModel.Instance.Pause();

                    CanvasPageViewModel.Instance.EscMenu.ToggleEscMenu();
                    break;
            }
        }

        private void CanvasPage_KeyUp(object sender, KeyEventArgs e)
            => _KeysDown.RemoveAll((key) => key == e.Key);



        // Considering re-implimenting this in some form, not sure if this previously messed with mouse down on individual ellpises on the canvas
        [Obsolete]
        private void FocusOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Clicked Point Relative to the top-left canvas corner
            Point MousePoint = e.GetPosition((ItemsControl)sender);

            // Clicked Point Relative to 0,0
            Vector RealPosition = Helpers.Centrlize(new Vector(MousePoint.X, MousePoint.Y), Helpers.CanvasOrigin.TopLeft);

            CanvasPageViewModel.FocusOnPoint(new Vector(RealPosition.X, RealPosition.Y));
        }
    }
}