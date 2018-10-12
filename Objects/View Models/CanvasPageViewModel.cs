using System.Collections.ObjectModel;
using System.Windows.Media;

namespace OrbitalSimulator_Objects
{
    public class CanvasPageViewModel : BaseViewModel
    {
        ObservableCollection<InterstellaObjectViewModel> _CanvasInterstellaObjects;

        public InterstellaObjectViewModel TestObject { get; set; }

        public CanvasPageViewModel()
        {
            CanvasInterstellaObjects = new ObservableCollection<InterstellaObjectViewModel>();

            //  This is only here for testing purposes and
            //  should be moved onto where ever the InterstellaObjects are parsed into the VM
            InterstellaObjectParams myParams =
                new InterstellaObjectParams(
                new Vector(200, 200),
                new Vector(0, 0),
                new Vector(0, 0),
                InterstellaObjectType.EarthSizedPlannet
            );

            InterstellaObject myPlanet = new InterstellaObject(myParams);

            CanvasInterstellaObjects.Add(new InterstellaObjectViewModel(new InterstellaObject(myParams)));


            InterstellaObjectParams myParams2 =
                new InterstellaObjectParams(
                new Vector(400, 200),
                new Vector(0, 0),
                new Vector(0, 0),
                InterstellaObjectType.EarthSizedPlannet
            );

            CanvasInterstellaObjects.Add(new InterstellaObjectViewModel(new InterstellaObject(myParams2)));
        }

        public ObservableCollection<InterstellaObjectViewModel> CanvasInterstellaObjects{ get => _CanvasInterstellaObjects; set => _CanvasInterstellaObjects = value; }
    }
}