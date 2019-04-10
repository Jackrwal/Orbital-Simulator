using JWOrbitalSimulatorPortable.ViewModels;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrbitalSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MasterViewModel NewMasterViewModel = new MasterViewModel();

            NewMasterViewModel.WindowHeight = (int)(SystemParameters.WorkArea.Height);
            NewMasterViewModel.WindowWidth = (int)(SystemParameters.WorkArea.Width);

            DataContext = NewMasterViewModel;

           
        }
    }
}
