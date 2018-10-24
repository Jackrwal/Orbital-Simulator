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
    public class CanvasPageViewModel : BaseViewModel
    {
        private InterstellaSystem _System;

        /// <summary>
        /// Construct a canvas with an existing system
        /// </summary>
        /// <param name="system"></param>
        public CanvasPageViewModel(InterstellaSystem system)
        {
            initialiseSystem(system);
        }

        /// <summary>
        /// Construct a canvas with a new blank system
        /// </summary>
        public CanvasPageViewModel()
        {
            initialiseSystem(new InterstellaSystem());
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
                return;
            }

            InterstellaObjectParams myParams =
                new InterstellaObjectParams(
                new Vector(20, 20),
                new Vector(10, -5),
                new Vector(2, 2),
                InterstellaObjectType.EarthSizedPlannet
            );

            InterstellaObjectParams myParams2 = new InterstellaObjectParams(
                new Vector(400, 200),
                new Vector(-10, 10),
                new Vector(0, 0),
                InterstellaObjectType.Star
            );

            initialiseSystem(new InterstellaSystem());

            AddObject(new InterstellaObject(myParams));
            AddObject(new InterstellaObject(myParams2));

            _System.Start();
        }

        public void AddObject(InterstellaObject newInterstellaObject)
        {
            _System.AddObject(newInterstellaObject);
            CanvasObjects.Add(new InterstellaObjectViewModel(newInterstellaObject));
        }

        public ObservableCollection<InterstellaObjectViewModel> CanvasObjects { get; set; }

        //Commands
        public ICommand StartSystem { get; set; }
        public ICommand StopSystem { get; set; }

        private void initialiseSystem(InterstellaSystem system)
        {
            _System = system;
            CanvasObjects = new ObservableCollection<InterstellaObjectViewModel>();

            StartSystem = new RelayCommand(_System.Start);
            StopSystem = new RelayCommand(_System.Stop);
        }

    }
}
