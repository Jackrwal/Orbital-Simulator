using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Media;

// !! Making CanvasPageViewModel Reflect the Model

// To make CanvasInterstellaObjects reflect the model i need to have a model collection representing all InterstellaObjects In the System
// I Suggest creating a new Model class ' InterstellaSystem ' 
// This will expose a collection of InterstellaObjects Which can be cast too InterstellaObjectViewModel's Using .Select and populate the observable collection.
// And can also make use of File Handling to save and load systems. DONE

// This System Could Contain The Timer to regulary update the position of Objects or could be externally ticked 
// (Currently the InterstellaObject class has static methods to tick every InterstellaObject, Alternativly if this is handled in system you could run multiple systems
//  And select which Objects you tick appropriatly) DONE

// in which teir of the MVVM model should the Over all control over the model (Mosly the timer) reside?
// InterstellaSystem Holds a timer which handles ticking each object to update its position,
// This is event driven so an event in the UI will cause the systems clock to be started.

// How do i get the Objects from my Model and into Canvas Interstella Objects?
// What ever loads the CanvasPage Should Load a system into the Canvas via this VM's ctor

// This means that what ever loads the Canvas Page must be able to parse in a InterstellaSystem to the CanvasPageViewModel.
// Currently this is handled directly by a Value Converter returning a blank CanvasPage (Which triggers the creation of a new blank VM in base page)
// Ultimatly the Switch in this Value Converter must be able to load a system from the WindowViewModel.
// This could involve calling some functions in the WindowViewModel to get the system which should be loaded.
// This requires the Value Converter being aware of the Window View Model.

// Alternativly too making the Value Conveter aware of the WindowViewModel When i handle the call back for navigate i could let the Converter Load a blank canvas
// And then load a System Into it in the navigation call back.  (I think i preffer this one)

namespace OrbitalSimulator_Objects
{
    public class CanvasPageViewModel : BaseViewModel
    {
        private InterstellaSystem _System;
        private ObservableCollection<InterstellaObjectViewModel> _CanvasInterstellaObjects;

        public CanvasPageViewModel(InterstellaSystem systemViewModel)
        {
            _System = systemViewModel;

            _CanvasInterstellaObjects = new ObservableCollection<InterstellaObjectViewModel>(_System.InterstellaObjects.Select(
                InterstellaObject => new InterstellaObjectViewModel(InterstellaObject)
            ));

        }

        /// <summary>
        /// Debug Constructor Loads some objects into the VM and runs the system
        /// </summary>
        public CanvasPageViewModel()
        {
            InterstellaObjectParams myParams =
                new InterstellaObjectParams(
                new Vector(0, 0),
                new Vector(10, 0),
                new Vector(0, 0),
                InterstellaObjectType.EarthSizedPlannet
            );

            InterstellaObject myPlanet = new InterstellaObject(myParams);

            InterstellaObjectParams myParams2 =
                new InterstellaObjectParams(
                new Vector(400, 200),
                new Vector(0, 10),
                new Vector(0, 0),
                InterstellaObjectType.Star
            );

            List<InterstellaObject> SystemObjects = new List<InterstellaObject>();
            SystemObjects.Add(new InterstellaObject(myParams));
            SystemObjects.Add(new InterstellaObject(myParams2));

            InterstellaSystem mySystem = new InterstellaSystem(SystemObjects);

            _System = mySystem;

            _CanvasInterstellaObjects = new ObservableCollection<InterstellaObjectViewModel>(
                _System.InterstellaObjects.Select(InterstellaObject => new InterstellaObjectViewModel(InterstellaObject))
            );

            _System.Start();
        }

        public ObservableCollection<InterstellaObjectViewModel> CanvasInterstellaObjects
        {
            get => _CanvasInterstellaObjects;
            set
            {
                _CanvasInterstellaObjects = value;
                NotifyPropertyChanged(this, nameof(CanvasInterstellaObjects));
            }
        }

    }
}