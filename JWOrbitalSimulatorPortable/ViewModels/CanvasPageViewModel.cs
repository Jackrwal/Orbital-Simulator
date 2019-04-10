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
        static public bool UseLogarithmicSeperationScaling { get; set; } = false;

        static public bool UseLogarithmicRadiusScaling { get; set; } = true;

        static public CanvasPageViewModel Instance;

        // Virtual origin is displayed at the centre of the screen.
        static public Vector ConstantVirtualOrigin = new Vector(0, 0);

        static public bool FocusedOnObject = false;

        // The object to keep at the centre of the screen when focused on an object
        static public InterstellaObjectViewModel PanFocusObject { get; set; }

        // The Vector to translate the logical system by to move the desired point to the centre of the screen before displaying.
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

        /// <summary>
        /// Move by translating it by a given vector
        /// </summary>
        /// <param name="panVector"> The Vector to move the Virtual orgin by </param>
        static public void Pan(Vector panVector)
        {
            if (FocusedOnObject)
            {
                ConstantVirtualOrigin = PanFocusObject.Position;
                FocusedOnObject = false;
            }

            ConstantVirtualOrigin += panVector;
        }

        // The Master Zoom of the application.
        static private double _MasterScale = 1;
        static public double MasterScale { get { return _MasterScale; } set { _MasterScale = value; } }

        // Variable used to control the linear scaling of object's position vectors
        static public double LinearObjectSeperation { get; set; } = 8E8;

        // Variables used to control the seperation, and how squeezed in the inside and outside of the system are, when using logarithmic scaling for object's position.
        static public double LogObjectPinch { get; set; } = 1.2E12;
        static public double LogObjectSeperation { get; set; } = 3200;

        // Variables used to control seperation for linear and logarithmic radius scaling methods
        static public double LogObjectRadius { get; set; } = 2E-7;
        static public double LinearObjectRadius { get; set; } = 1E6;

        // The Inverse methods for linear scaling used to get the logical position of objects dropped at a screen position.
        static public double InverseSeperationScaler(double distance) => distance * (8E8 * (1 / _MasterScale));
        static public Vector InverseSeperationScaler(Vector distance) => distance * (8E8 * (1 / _MasterScale));

        // Logarithmic Seperation Scaling function
        // see https://www.desmos.com/calculator/srzed79qph for scaler function model.
        static public double SeperationScaler(double distance)
        {
            double k = LogObjectPinch;      
            double M = LogObjectSeperation; 

            if (UseLogarithmicSeperationScaling)
            {
                // y = M log(1/k * |x| + 1)
                if (distance >= 0) return M * Math.Log10((_MasterScale * (1 / k) * distance) + 1);

                // -Log(baseScale * |radius|) for negative values
                else return - M * Math.Log10((_MasterScale * (1/k) * Math.Abs(distance)) + 1);
            }

            else return _MasterScale * (distance / LinearObjectSeperation);
        }

        // Logarithmic Seperation Scaler
        static public Vector SeperationScaler(Vector distance)
        {
     
            if (UseLogarithmicSeperationScaling)
            {
                // Seperate the vector into a magnitude and a direction
                double Magnitude = distance.Magnitude;
                Vector Unit = distance / distance.Magnitude;

                // Logarithmically scale the magnitude then give the new mangitude the direction of the vector.
                return SeperationScaler(Magnitude) * Unit;
            }
            else
                return _MasterScale * (distance / LinearObjectSeperation);
        }

        // Scaling the size of objects on a logarithmic scale
        static public double RadiusScale(double radius)
        {
            if (UseLogarithmicRadiusScaling) return 60 * _MasterScale * Math.Log10((LogObjectRadius * radius) + 1);

            else return _MasterScale * (radius / LinearObjectRadius);

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

        // Canvas Side bar Width is 22% of the screen
        private double _CanvasSideBarWidthWeighting = 0.22;

        // ------------------------------------------------------------------------------------ Constructor --------------------------------------------------------------------------------------------

        /// <summary>
        /// Construct a canvas with a new blank system
        /// </summary>
        public CanvasPageViewModel()
        {
            // Layout Canvas
            CanvasHeight = MasterViewModel.Instance.WindowHeight;
            SideBarWidth = (int)(MasterViewModel.Instance.WindowWidth * _CanvasSideBarWidthWeighting);
            CanvasWidth = MasterViewModel.Instance.WindowWidth - SideBarWidth;

            // Set Instance
            Instance = this;

            // Load Escape Menu
            EscMenu = new CanvasEscMenuViewModel(0.4);

            // Load Data Box
            DataBoxVM = new HoverDataEntryBoxViewModel();

            // Load SaveNameBox
            SaveNameBox = new EnterSaveNameBoxViewModel(0.2);

            // Set Commands
            OpenDataEntryBox = new ParamRelayCommand<InterstellaObjectViewModel>(new Action<InterstellaObjectViewModel>(OpenDataBox));
            HideDataEntryBox = new RelayCommand(new Action(HideDataBox));

            // Load the system
            loadSystem(new InterstellaSystem());

            // Load Side Bar
            initialiseSideBar();
        }

        // ----------------------------------------------------------------------------------- Public Methods ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Load a new blank logical system 
        /// </summary>
        public void LoadNewSystem()
        {
            loadSystem(new InterstellaSystem());
        }

        /// <summary>
        /// Load a logical system from a save file and display it to canvas page
        /// </summary>
        /// <param name="fileString">Path of system file to load</param>
        public void LoadSystem(string fileString)
        {
            loadSystem(SystemFileParser.ReadSystemFile(fileString));

            UpdateSystemPannelObjects();
        }

        /// <summary>
        /// Display a given logical system to the canvaspage.
        /// </summary>
        /// <param name="system">Logical System to display</param>
        public void LoadSystem(InterstellaSystem system)
        {
            loadSystem(system);

            UpdateSystemPannelObjects();
        }

        /// <summary>
        /// Add an obect to the logical system being displayed by the canvas page.
        /// </summary>
        /// <param name="newInterstellaObject"></param>
        public void AddObject(InterstellaObject newInterstellaObject)
        {
            System.AddObject(newInterstellaObject);

            InterstellaObjectViewModel newObjectVm = new InterstellaObjectViewModel(newInterstellaObject);
            CanvasObjects.Add(newObjectVm);

            NotifyPropertyChanged(this, nameof(CanvasObjects));

            SideBarVM.InfoPannelVM.AddDisplayObject(newObjectVm);
        }

        /// <summary>
        /// Start the logical system running
        /// </summary>
        public void Play()
        {
            if (!SystemRunning) SystemRunning = true;
            else return;
        }

        /// <summary>
        /// Stop the logical system running
        /// </summary>
        public void Pause()
        {
            if (SystemRunning) SystemRunning = false;
            else return;
        }

        // ------------------------------------------------------------------------------------ Properties -------------------------------------------------------------------------------------------------

        // Commands to show and hide the hover data entry box.
        public ICommand OpenDataEntryBox;
        public ICommand HideDataEntryBox;

        public double SystemSpeed { get { return System.TimeMultiplier / 1E6; } set { System.TimeMultiplier = value * 1E6; } }
        public bool SystemRunning { get { return System.RunSys; } set { System.RunSys = value; } }

        public bool DataBoxVisible => DataBoxVM.Visibility;

        /// <summary>
        /// The Objects to be displayed on the canvas
        /// </summary>
        public ObservableCollection<InterstellaObjectViewModel> CanvasObjects
        {
            get { return new ObservableCollection<InterstellaObjectViewModel>(System.InterstellaObjects.Select((SystemObject) => new InterstellaObjectViewModel(SystemObject))); }
        }

        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }

        public int SideBarWidth { get; set; }

        /// <summary>
        /// View Model to Control the content of the side bar control
        /// </summary>
        public CanvasSideBarViewModel SideBarVM { get; set; }

        /// <summary>
        /// Holds the instance of the data entry box to display around the screen
        /// </summary>
        public HoverDataEntryBoxViewModel DataBoxVM { get; set; }

        /// <summary>
        /// Holds the instance of the CanvasEscMenuViewModel controling the CanvasPage's EscMenu Control
        /// </summary>
        public CanvasEscMenuViewModel EscMenu { get; set; }

        /// <summary>
        /// Instance of the EnterSaveNameBoxViewModel controling the canvasPage's Save name Box.
        /// </summary>
        public EnterSaveNameBoxViewModel SaveNameBox { get; set; }

        // The logical system to display too screen
        public InterstellaSystem System { get => _System; set => _System = value; }

        // ------------------------------------------------------------------------------------ Private Methods --------------------------------------------------------------------------------------------

        private void OpenDataBox(InterstellaObjectViewModel ObjectVm)
        {
            DataBoxVM.DisplayEntryBox(ObjectVm);
        }

        private void HideDataBox()
        {
            DataBoxVM.HideBox();
        }

        /// <summary>
        /// Loads a system into the canvas page
        /// </summary>
        /// <param name="system"></param>
        private void loadSystem(InterstellaSystem system)
        {
            //Set the system displayed by the canvas page to be the new system
            System = system;

            //Generate View Models for the new system
            foreach (var Object in System.InterstellaObjects)
            {
                InterstellaObjectViewModel newVM = new InterstellaObjectViewModel(Object);
                CanvasObjects.Add(newVM);
            }
            NotifyPropertyChanged(this, nameof(CanvasObjects));

            //Subscribe onSystemCollectionAltered to the new systems collection altered event.
            System.SystemCollectionAltered += onSystemCollectionAltered;
        }

        /// <summary>
        /// When the Collection of objects in the system is altered notify the view to update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onSystemCollectionAltered(object sender, EventArgs e)
        {
            UpdateSystemPannelObjects();
            NotifyPropertyChanged(this, nameof(CanvasObjects));
        }

        /// <summary>
        /// Helper Method to set up the side bar control
        /// </summary>
        private void initialiseSideBar()
        {
            List<InterstellaDragDropViewModel> DragDropObjects = new List<InterstellaDragDropViewModel>();
        
            // Add the objects avaiable to drag and drop too the sidebar.
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.StarInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.RockyPlannetInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.EarthInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.MoonInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.AstaroidInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.GasGiantInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.IceGiantInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.DwarfPlannetInstance));

            // Initialise the SideBar VM with Avaliable dragdrop objects and the system being displayed
            SideBarVM = new CanvasSideBarViewModel(
                DragDropObjects,
                System
            );
        }

        /// <summary>
        /// Update the objects displayed in the InfoPannel to reflect the currently displayed system.
        /// </summary>
        private void UpdateSystemPannelObjects()
        {
            // Re-set the displayed collection of object
            SideBarVM.InfoPannelVM.InfoObjects = new ObservableCollection<InterstellaObjectViewModel>();

            // Add new viewmodels for every object in the system at this instance.
            foreach (var Object in System.InterstellaObjects)
            {
                SideBarVM.InfoPannelVM.AddDisplayObject(new InterstellaObjectViewModel(Object));
            }
        }
    }
}
