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
            InterstellaObject myPlanet = new InterstellaObject(0, 200, 200, 0, 0, 0.5, 0.5);
            InterstellaObjectViewModel PlanetVm = new InterstellaObjectViewModel(myPlanet);
            InitializeComponent();

           DataContext = PlanetVm;
        }
    }
}
