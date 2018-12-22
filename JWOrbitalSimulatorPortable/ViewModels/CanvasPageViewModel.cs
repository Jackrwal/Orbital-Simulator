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
        // Scaling the distance between objects on a linear scale
        static public double SeperationScaler(double distance) => distance/(4.42E8*(1/MasterScale));
        static public double InverseSeperationScaler(double distance) => distance * (4.42E8* (1/ MasterScale));

        // Scaling the size of objects on a logarithmic scale
        static public double RadiusScale(double radius)
        {
            if (radius >= 0) return 30 * MasterScale * Math.Log10((2E-7 * radius) + 1);

            // -Log(baseScale * |radius|) for negative values
            else return -(30 * MasterScale * Math.Log10((2E-7 * Math.Abs(radius)) + 1));
        }
        // Currenly the InverseScale might not work for negative raidus, however realistically the user wont be able to drop anything at a negative Co-Ords
        static public double InverseRadiusScale(double radius) => (Math.Pow(10, radius/(30 * MasterScale))-1) / 2E-7;

        static public double MasterScale = 0.5;

        private InterstellaSystem _System;

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

            InterstellaObjectParams myParams =
                new InterstellaObjectParams(
                new Vector(InverseSeperationScaler(800) + 1.5E11, InverseSeperationScaler(600)),
                new Vector(0, PlacementDeScale(110)),
                new Vector(0, 0),
                InterstellaObjectType.EarthSizedPlannet
            );

            InterstellaObjectParams myParams3 =
                new InterstellaObjectParams(
                new Vector(InverseSeperationScaler(800) - 1.8E11, InverseSeperationScaler(600)),
                new Vector(0, -PlacementDeScale(110)),
                new Vector(0, 0),
                InterstellaObjectType.EarthSizedPlannet
            );

            InterstellaObjectParams myParams2 = new InterstellaObjectParams(
                new Vector(InverseSeperationScaler(800), InverseSeperationScaler(600)),
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

        // ## Temporary function to control bodies starting velocity without impact from Master Scale
        private double PlacementDeScale(double radius) => (Math.Pow(10, radius / 30) - 1) / 2E-7;

        public void AddObject(InterstellaObject newInterstellaObject)
        {
            _System.AddObject(newInterstellaObject);
            CanvasObjects.Add(new InterstellaObjectViewModel(newInterstellaObject));
        }

        /// <summary>
        /// The Objects to be displayed on the canvas
        /// </summary>
        public ObservableCollection<InterstellaObjectViewModel> CanvasObjects { get; set; }

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
            CanvasObjects = new ObservableCollection<InterstellaObjectViewModel>();
        }

        /// <summary>
        /// Helper Method to set up the side bar control
        /// </summary>
        private void initialiseSideBar()
        {
            List<InterstellaDragDropViewModel> DragDropObjects = new List<InterstellaDragDropViewModel>();

            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.StarInstance));
            DragDropObjects.Add(new InterstellaDragDropViewModel(CanvasSideBarObjects.EarthInstance));

            SideBarVM = new CanvasSideBarViewModel(
                DragDropObjects, 
                new RelayCommand(_System.Start), 
                new RelayCommand(_System.Stop), 
                new RelayCommand(_System.Step)
            );

        }

    }
}
