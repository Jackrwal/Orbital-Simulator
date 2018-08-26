using OrbitalSimulator_Objects;
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

namespace OrbitalSimulator_UI.Views
{
    /// <summary>
    /// Interaction logic for CanvasPage.xaml
    /// </summary>
    public partial class CanvasPage : Page
    {
        public CanvasPage()
        {
            InitializeComponent();

            InterstellaObjectParams myParams =
                new InterstellaObjectParams(
                new OrbitalSimulator_Objects.Vector(20, 20),
                new OrbitalSimulator_Objects.Vector(2, 0),
                new OrbitalSimulator_Objects.Vector(0.5, 0),
                InterstellaObjectType.EarthSizedPlannet
            );

            InterstellaObject myPlanet = new InterstellaObject(myParams);


            DataContext = new InterstellaObjectViewModel(myPlanet);
        }
    }
}
