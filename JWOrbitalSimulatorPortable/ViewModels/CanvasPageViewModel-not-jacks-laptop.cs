using JWOrbitalSimulatorPortable.Commands;
using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class CanvasPageViewModel : NotifyingViewModel
    {
        static public Vector VirtualOrigin = new Vector(0,0);
        static public double XPan = 0, YPan = 0;

        static public Vector PanVector { get { return new Vector(XPan, YPan); } set { XPan = value.X; YPan = value.Y; } }

        // Scaling the distance between objects on a linear scale
        static public double SeperationScaler(double distance) => distance/(4.42E8*(1/_MasterScale));//4.42E8 base val
        static public double InverseSeperationScaler(double distance) => distance * (4.42E8* (1/ _MasterScale));

        // Scaling the size of objects on a logarithmic scale
        static public double RadiusScale(double radius)
        {
            if (radius >= 0) return 30 * _MasterScale * Math.Log10((2E-7 * radius) + 1);

            // -Log(baseScale * |radius|) for negative values
            else return -(30 * _MasterScale * Math.Log10((2E-7 * Math.Abs(radius)) + 1));
        }
        // Currenly the InverseScale might not work for negative raidus, however realistically the user wont be able to drop anything at a negative Co-Ords
        static public double InverseRadiusScale(double radius) => (Math.Pow(10, radius/(30 * _MasterScale))-1) / 2E-7;

        static private double _MasterScale = 1;
        static public double MasterScale { get { return _MasterScale; } set { _MasterScale = value; } }

        private InterstellaSystem _System;

        public double SystemSpeed { get { return _System.TimeMultiplier/1E6; } set { _System.TimeMultiplier = value*1E6;} }
        public bool SystemRunning { get { return _System.RunSys; } set { _System.RunSys = value; } }


        /// <summary>
        /// Construct a canvas with an existing system
        /// </summary>
        /// <param name="system"></param>
        public CanvasPageViewModel(InterstellaSystem system)
        {
            initialiseSystem(system);
            initialiseSideBar();
        }

        /// <summary>
        /// Construct a canvas with a new blank system
        /// </summary>
        public CanvasPageViewModel()
        {
            initialiseSystem(new InterstellaSystem());
            initialiseSideBar();
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
                new Vector(1.5E11, 0),
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

            AddObject(new InterstellaObject(myParams3));
            AddObject(new InterstellaObject(myParams2));
            AddObject(new InterstellaObject(myParams));
        }

        public void AddObject(InterstellaObject newInterstellaObject)
        {
            _System.AddObject(newInterstellaObject);
            CanvasObjects.Add(new InterstellaObjectViewModel(newInterstellaObject));
            NotifyPropertyChanged(this, nameof(CanvasObjects));
        }

        /// <summary>
        /// The Objects to be displayed on the canvas
        /// </summary>
        public ObservableCollection<InterstellaObjectViewModel> CanvasObjects
        {
            get { return new ObservableCollection<InterstellaObjectViewModel>(_System.InterstellaObjects.Select((SystemObject) => new InterstellaObjectViewModel(SystemObject))); }
        }

        /// <summary>
        /// View Model to Control the content of the side bar control
        /// </summary>
        public CanvasSideBarViewModel SideBarVM { get; set; }

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
