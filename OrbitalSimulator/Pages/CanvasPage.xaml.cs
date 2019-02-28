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
        private double _PanAmount;
        private Key[] _PanKeys = new Key[] { Key.W, Key.A, Key.S, Key.D, Key.Up, Key.Down, Key.Left, Key.Right };
        private List<Key> _KeysDown = new List<Key>();
        private Point _MouseDownPoint;
        private Point _MouseReleasePoint;

        // ## FIX WHEN FILE LOADING Curently Loading Test Sytem in CPVM remove this later to add objects via the UI or file load 
        public CanvasPage() : base(new CanvasPageViewModel(true))
        {
            InitializeComponent();
        }


        // Handels dropping a new object on to the canvas
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

            DroppedObject.Position = DropPositionVector;
            _VM.AddObject(new InterstellaObject(DroppedObject));
        }

        // ~~ Add Infomation Display Box on Hover
        private void EditObjectOnRightClick(object sender, MouseButtonEventArgs e)
        {
            InterstellaObjectViewModel SenderAsVm = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;
            // ~~ On Right Click Pause and allow to change object values
            // Pause System
            CanvasPageViewModel.Instance.SystemRunning = false;

            // Make Hover Pop-Up Persist outside of hover. (Should this be a different but same looking pop-up that takes over seemlessly?)
            // Call an ICommand from CanvasPageViewModel too handle the view model side of this, This should require the object that was clicked on

            // Allow Values to be entered into pop up. Or Drag to add velocity 
        }

        private void FocusAtObjectOnLeftClick(object sender, MouseButtonEventArgs e)
        {
            InterstellaObjectViewModel ObjectVm = (InterstellaObjectViewModel)((Ellipse)sender).DataContext;
            CanvasPageViewModel.FocusOnObject(ObjectVm);
        }

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
                    if(!e.IsRepeat && DataContextViewModel.SystemRunning) DataContextViewModel.SystemRunning = false;
                    else if(!e.IsRepeat) DataContextViewModel.SystemRunning = true;
                    break;
            }
        }

        private void CanvasPage_KeyUp(object sender, KeyEventArgs e) => _KeysDown.Remove(e.Key);

        // ~~ On Mouse Down and Up events pan by drag (by shifting the virtual origin by change in mouse position) (Maybe on Shift or CTRL click)

        // On Mouse Wheel Zoom about the virtual origin
        private void ZoomOnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double ZoomIncr = ((double)e.Delta) / 1000;

            // Check Zoom Incriment leaves zoom with in a suitable range
            if ((CanvasPageViewModel.MasterScale + ZoomIncr) <= 10 && (CanvasPageViewModel.MasterScale + ZoomIncr) >= 0.1)
                CanvasPageViewModel.MasterScale += ZoomIncr;
        }

        private void CanvasPage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _MouseReleasePoint = e.GetPosition(this);
            // Deal with drag Pan.
        }

        private void CanvasPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _MouseDownPoint = e.GetPosition(this);
        }

        [Obsolete]
        private void FocusOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Clicked Point Relative to the top-left canvas corner
            Point MousePoint = e.GetPosition((ItemsControl)sender);

            // ~~ Virutal origin still puts the test star at 0,0 130 below the canvas center

            // Clicked Point Relative to 0,0
            Vector RealPosition = CanvasHelpers.Centrlize(new Vector(MousePoint.X, MousePoint.Y), CanvasHelpers.CanvasOrigin.TopLeft);

            CanvasPageViewModel.FocusOnPoint(new Vector(RealPosition.X, RealPosition.Y));
        }
    }
}
