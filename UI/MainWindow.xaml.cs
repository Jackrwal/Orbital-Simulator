using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            InterstellaObject myPlanet = new InterstellaObject(0, 20, 20, 0, 0, 0.5, 0.5);
            InterstellaObjectViewModel PlanetVm = new InterstellaObjectViewModel(myPlanet);
            InitializeComponent();
            
        }
    }
}
