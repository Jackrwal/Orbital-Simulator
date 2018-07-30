using System.Windows;
using OrbitalSimulator_Objects;

namespace OrbitalSimulator_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InterstellaObjectParams myParams = 
                new InterstellaObjectParams(
                    new OrbitalSimulator_Objects.Vector(0, 0), 
                    new OrbitalSimulator_Objects.Vector(0, 0), 
                    new OrbitalSimulator_Objects.Vector(0, 0), 
                    InterstellaObjectType.EarthSizedPlannet);

            InterstellaObject myPlanet = new InterstellaObject(myParams);

            InterstellaObjectViewModel PlanetVm = new InterstellaObjectViewModel(myPlanet);
            InitializeComponent();

            DataContext = PlanetVm;
        }
    }
}
