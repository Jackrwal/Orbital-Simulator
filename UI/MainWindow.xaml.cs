using System.Windows;
using OrbitalSimulator_Objects;
using System.Threading;

// ## TODO:
//
//    Add a Style for an InterstellaObject in the view, Content Presenter should get an Elipse by binding to a Type Property in the InterstellaObjectViewModel 
//    Returning a Bound Elipse from InterstellaObjectTypeValueConverter
//
//    Add an ItemControl to display a collection of InterstellaObejctViewModels by binding to the SystemViewModel
//    see: https://stackoverflow.com/questions/2317713/binding-wpf-canvas-children-to-an-observablecollection

namespace OrbitalSimulator_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   

            InitializeComponent();

            WindowViewModel WVM = new WindowViewModel();
            DataContext = WVM;

        }
    }
}
    