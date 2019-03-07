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

// ##  This class is getting really big,
// Maybe i could make the canvas into its own user control w its own view model and just deal with UI stuff here.

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class CanvasPageViewModel : NotifyingViewModel
    {
        // ------------------------------------------------------------------------------------ Static components -------------------------------------------------------------------------------------------------

        static public CanvasPageViewModel Instance;

        // Virtual origin is displayed at the centre of the screen.
        static public Vector ConstantVirtualOrigin = new Vector(0,0);

        static public bool FocusedOnObject = false;
        static public InterstellaObjectViewModel PanFocusObject { get; set; }

        static public Vector PanVector
        {
            get
            {
                if (FocusedOnObject)
                {
                    if (PanFocusObject == null) return ConstantVirtualOrigin;
                    return PanFocusObject.Position * -1;
                }
                else return new Vector(-ConstantVirtualOrigin.X, -ConstantVirtualOrigin.Y);
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
            ConstantVirtualOrigin += focalPoint;
            FocusedOnObject = false;
        }

        static public void Pan(Vector panVector)
        {
            if(FocusedOnObject)
            {
                ConstantVirtualOrigin = PanFocusObject.Position;
                FocusedOnObject = false;
            }

            ConstantVirtualOrigin += panVector;
        }

        static private double _MasterScale = 1;
        static public double MasterScale { get { return _MasterScale; } set { _MasterScale = value; } }

        // Scaling the distance between objects on a linear scale
        static public double SeperationScaler(double distance) => distance / (4.42E8 * (1 /_MasterScale));//4.42E8 base val
        static public Vector SeperationScaler(Vector distance) => distance / (4.42E8 * (1 / _MasterScale));//4.42E8 base val
 
        static public double InverseSeperationScaler(double distance) => distance * (4.42E8* (1 / _MasterScale));
        static public Vector InverseSeperationScaler(Vector distance) => distance * (4.42E8 * (1 / _MasterScale));

        // Scaling the size of objects on a logarithmic scale
        static public double RadiusScale(double radius)
        {
            if (radius >= 0) return 30 * _MasterScale * Math.Log10((2E-7 * radius) + 1);

            // -Log(baseScale * |radius|) for negative values
            else return -(30 * _MasterScale * Math.Log10((2E-7 * Math.Abs(radius)) + 1));
        }

        /// <summary>
        /// Get the velocity an object needs to orbit a second object at its current distance
        /// </summary>
        /// <param name="interstellaObject"></param>
        /// <param name="objectToOrbit"></param>
        static public Vector GetOrbitVelocity(InterstellaObject interstellaObject, InterstellaObject objectToOrbit)
        {
            // Seperation, Large object - small object
            Vector R1R2 = objectToOrbit.Position - interstellaObject.Position;
            Vector R1R2Unit = R1R2 / R1R2.Magnitude;

            // Calculate Magnitude Of Velocity, V = GM/|r|
            double VectorMag = Math.Sqrt((6.674E-11 * objectToOrbit.Mass) / R1R2.Magnitude);

            // Velocity should be at a normal to the force. So multiply by normal to force unit vector.
            Vector Velocity = VectorMag * R1R2Unit.Normal;

            // Randomize orbit direction and Add the velocity of the obejct to orbit too the relative velocity to orbit.
            if (Helpers.RNG.Next(0, 1) == 1) Velocity += objectToOrbit.Velocity;

            else Velocity = (-1 * Velocity) + objectToOrbit.Velocity;

            return Velocity;
        }

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
            initialiseSystem(system);
            Layout();
            SetUpDataBox();
            initialiseSideBar();

            Instance = this;
        }

        /// <summary>
        /// Construct a canvas with a new blank system
        /// </summary>
        public CanvasPageViewModel()
        {
            initialiseSystem(new InterstellaSystem());
            Layout();
            Instance = this;
            initialiseSideBar();
            SetCommands();
            SetUpDataBox();

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

            //Load Test System From File
            List<string> ReadableSaveFiles = SystemFileParser.GetReadableSaveFiles();
            LoadSystem(ReadableSaveFiles[0]);
            
            Layout();
            SetCommands();
            Instance = this;
            initialiseSideBar();
            SetUpDataBox();
        }

        // ----------------------------------------------------------------------------------- Public Methods ---------------------------------------------------------------------------------------------

        public void LoadNewSystem()
        {
            initialiseSystem(new InterstellaSystem());
        }

        public void LoadSystem(string fileString)
        {
            initialiseSystem(SystemFileParser.ReadSystemFile(fileString));
        }

        public void AddObject(InterstellaObject newInterstellaObject)
        {
            _System.AddObject(newInterstellaObject);

            InterstellaObjectViewModel newObjectVm = new InterstellaObjectViewModel(newInterstellaObject);
            CanvasObjects.Add(newObjectVm);

            NotifyPropertyChanged(this, nameof(CanvasObjects));

            SideBarVM.InfoPannelVM.AddDisplayObject(newObjectVm);
        }

        // ------------------------------------------------------------------------------------ Properties -------------------------------------------------------------------------------------------------
        
        public ICommand OpenDataEntryBox;
        public ICommand HideDataEntryBox;

        public double SystemSpeed { get { return _System.TimeMultiplier / 1E6; } set { _System.TimeMultiplier = value * 1E6; } }
        public bool SystemRunning { get { return _System.RunSys; } set { _System.RunSys = value; } }

        public bool DataBoxVisible => DataBoxVM.Visibility;

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

        /// <summary>
        /// Holds the instnae of the data entry box to display around the screen
        /// </summary>
        public HoverDataEntryBoxViewModel DataBoxVM { get; set; }

        // ------------------------------------------------------------------------------------ Private Methods --------------------------------------------------------------------------------------------

        private void SetUpDataBox()
        {
            DataBoxVM = new HoverDataEntryBoxViewModel();
        }

        private void OpenDataBox(InterstellaObjectViewModel ObjectVm)
        {
            DataBoxVM.DisplayEntryBox(ObjectVm);
        }

        private void HideDataBox()
        {
            DataBoxVM.HideBox();
        }

        private void SetCommands()
        {
            OpenDataEntryBox = new ParamRelayCommand<InterstellaObjectViewModel>(new Action<InterstellaObjectViewModel>(OpenDataBox));
            HideDataEntryBox = new RelayCommand(new Action(HideDataBox));
        }

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

            foreach (var Object in _System.InterstellaObjects)
            {
                InterstellaObjectViewModel newObjectVm = new InterstellaObjectViewModel(Object);
                CanvasObjects.Add(newObjectVm);
            }
            NotifyPropertyChanged(this, nameof(CanvasObjects));

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

            foreach (var Object in _System.InterstellaObjects)
            {
                SideBarVM.InfoPannelVM.AddDisplayObject(new InterstellaObjectViewModel(Object));
            }

        }

    }
}
