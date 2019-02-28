using JWOrbitalSimulatorPortable.Commands;
using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

//~~ Notfiy View of changes impacting obect location and size like zoom, focus and pan so it will re-draw when paused.

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class CanvasPageViewModel : NotifyingViewModel
    {
        // ------------------------------------------------------------------------------------ Static components -------------------------------------------------------------------------------------------------

        static public CanvasPageViewModel Instance;

        // ~~ Virutal origin still puts the test star at 0,0 130 below the canvas center
        // Virtual origin is displayed at the centre of the screen.
        static public Vector VirtualOrigin = new Vector(0,0);

        static public bool FocusedOnObject = false;
        static public InterstellaObjectViewModel PanFocusObject { get; set; }

        static public Vector PanVector
        {
            get
            {
                if (FocusedOnObject)
                {
                    // ~~ Im pretty sure that when i click an object, it is registering the click on both the canvas and the object.
                    //    There for it is focusing on that object then immediatly back onto the point clicked where the object was

                    return PanFocusObject.Position * -1;
                }
                else
                {
                    return new Vector(-VirtualOrigin.X, -VirtualOrigin.Y);
                }
            }
        }

        /// <summary>
        /// Sets the virtual origin to always reflect position of an object.
        /// This Object will always be at the centre of the canvas.
        /// </summary>
        /// <param name="FocusableObject"></param>
        static public void FocusOnObject(InterstellaObjectViewModel focusableObject)
        {
            FocusedOnObject = true;
            PanFocusObject = focusableObject;
        }

        /// <summary>
        /// Sets the virtual origin to always reflect a point in the model.
        /// This Point will always appear at the centre of the screen
        /// </summary>
        /// <param name="focalPoint"></param>
        static public void FocusOnPoint(Vector focalPoint)
        {
            VirtualOrigin += focalPoint;
            FocusedOnObject = false;
        }

        static public void Pan(Vector panVector)
        {
            if (FocusedOnObject)
            {
                // Set VO too where it was previously focusing but stop tracking, so screen doesnt jump back to 0,0 affter stopping tracking on pan.
                VirtualOrigin = PanFocusObject.Position;
                FocusedOnObject = false;
            }

            VirtualOrigin += panVector;
        }

        // Scaling the distance between objects on a linear scale
        static public double SeperationScaler(double distance) => distance/(4.42E8*(1/ MasterScale));//4.42E8 base val
        static public Vector SeperationScaler(Vector distance) => distance / (4.42E8 * (1 / MasterScale));//4.42E8 base val

        static public double InverseSeperationScaler(double distance) => distance * (4.42E8* (1/ MasterScale));

        // Scaling the size of objects on a logarithmic scale
        static public double RadiusScale(double radius)
        {
            if (radius >= 0) return 30 * MasterScale * Math.Log10((2E-7 * radius) + 1);

            // -Log(baseScale * |radius|) for negative values
            else return -(30 * MasterScale * Math.Log10((2E-7 * Math.Abs(radius)) + 1));
        }

        static public double MasterScale { get; set; }

        // ------------------------------------------------------------------------------------ Fields -------------------------------------------------------------------------------------------------

        private InterstellaSystem _System;
        private double _CanvasSideBarWidthWeighting = 0.125;

        // ------------------------------------------------------------------------------------ Constructors --------------------------------------------------------------------------------------------

        /// <summary>
        /// Construct a canvas with an existing system
        /// </summary>
        /// <param name="system"></param>
        public CanvasPageViewModel(InterstellaSystem system)
        {
            Layout();
            initialiseSystem(system);
            initialiseSideBar();
            Layout();

            Instance = this;
        }

        /// <summary>
        /// Construct a canvas with a new blank system
        /// </summary>
        public CanvasPageViewModel()
        {
            initialiseSystem(new InterstellaSystem());
            initialiseSideBar();
            Layout();

            Instance = this;
        }

        /// <summary>
        /// Constructor for initialising a canvas with a testing system loaded
        /// </summary>
        /// <param name="systemTest"></param>
        public CanvasPageViewModel(bool systemTest)
        {
            if (!systemTest)
            {
                initialiseSystem(new InterstellaSystem());
                initialiseSideBar();
                return;
            }

            #region Collision Detection Test
            //InterstellaObjectParams myParams =
            //    new InterstellaObjectParams(
            //    new Vector(1.5E11, InverseSeperationScaler(300)),
            //    new Vector(0, 0),
            //    new Vector(0, 0),
            //    InterstellaObjectType.Star
            //);

            //InterstellaObjectParams myParams2 = new InterstellaObjectParams(
            //    new Vector(0, InverseSeperationScaler(300)),
            //    new Vector(0, 0),
            //    new Vector(0, 0),
            //    InterstellaObjectType.Star
            //);
            #endregion

            // Orbit Physics Test, using real physics values
            InterstellaObjectParams myParams =
                new InterstellaObjectParams(
                new Vector(1.5E11 + 1.5E11, 1.5E11),  // 1.5E11
                new Vector(0, 3E4), //3E4
                new Vector(0, 0),
                InterstellaObjectType.EarthSizedPlannet
            );

            InterstellaObjectParams myParams2 =
                new InterstellaObjectParams(
                new Vector(1.5E11 + 3.844E8 + (6.3E6 + 1.74E6), 0),
                new Vector(0, 3E4 + 931),
                new Vector(0, 0),
                InterstellaObjectType.Moon
            );

            InterstellaObjectParams myParams3 = new InterstellaObjectParams(
                new Vector(0, 0), 
                new Vector(0, 0),
                new Vector(0, 0),
                InterstellaObjectType.Star
            );

            initialiseSystem(new InterstellaSystem());
            initialiseSideBar();
            Layout();
            Instance = this;

            AddObject(new InterstellaObject(myParams3));
            AddObject(new InterstellaObject(myParams2));
            AddObject(new InterstellaObject(myParams));
        }

        // ----------------------------------------------------------------------------------- Public Methods ---------------------------------------------------------------------------------------------

        public void AddObject(InterstellaObject newInterstellaObject)
        {
            _System.AddObject(newInterstellaObject);
            CanvasObjects.Add(new InterstellaObjectViewModel(newInterstellaObject));
            NotifyPropertyChanged(this, nameof(CanvasObjects));
        }

        // ------------------------------------------------------------------------------------ Properties -------------------------------------------------------------------------------------------------

        public double SystemSpeed { get { return _System.TimeMultiplier / 1E6; } set { _System.TimeMultiplier = value * 1E6; } }
        public bool SystemRunning { get { return _System.RunSys; } set { _System.RunSys = value; } }

        /// <summary>
        /// The Objects to be displayed on the canvas
        /// </summary>
        public ObservableCollection<InterstellaObjectViewModel> CanvasObjects
        {
            get { return new ObservableCollection<InterstellaObjectViewModel>(_System.InterstellaObjects.Select((SystemObject) => new InterstellaObjectViewModel(SystemObject))); }
        }

        public int CanvasWidth  { get; set; }
        public int CanvasHeight { get; set; }

        public int SideBarWidth { get; set; }

        /// <summary>
        /// View Model to Control the content of the side bar control
        /// </summary>
        public CanvasSideBarViewModel SideBarVM { get; set; }

        // ------------------------------------------------------------------------------------ Private Methods --------------------------------------------------------------------------------------------

        /// <summary>
        /// Helper Method to layout the dimensions of the canvas and sidebar based on the Main Window Dimensions.
        /// Used on construction or Window layout updated.
        /// </summary>
        private void Layout()
        {
            CanvasHeight = MasterViewModel.Instance.WindowHeight;

            SideBarWidth = (int)(MasterViewModel.Instance.WindowWidth * _CanvasSideBarWidthWeighting);
            CanvasWidth = MasterViewModel.Instance.WindowWidth - SideBarWidth;
        }

        /// <summary>
        /// Helper Method to add a systems objects to the canvas and link the canvas' commands to system methods
        /// </summary>
        /// <param name="system"></param>
        private void initialiseSystem(InterstellaSystem system)
        {
            _System = system;
            //CanvasObjects = new ObservableCollection<InterstellaObjectViewModel>();
            _System.SystemCollectionAltered += onSystemCollectionAltered;
        }

        /// <summary>
        /// When the Collection of objects in the system is altered notify the view to update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onSystemCollectionAltered(object sender, EventArgs e) => NotifyPropertyChanged(this, nameof(CanvasObjects)); 

        /// <summary>
        /// Helper Method to set up the side bar control
        /// </summary>
        private void initialiseSideBar()
        {
            List<InterstellaDragDropViewModel> DragDropObjects = new List<InterstellaDragDropViewModel>();

            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.StarInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.EarthInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.MoonInstance));

            SideBarVM = new CanvasSideBarViewModel(
                DragDropObjects, 
                _System
            );

        }

    }
}
