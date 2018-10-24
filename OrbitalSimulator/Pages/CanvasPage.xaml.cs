using JWOrbitalSimulatorPortable.ViewModels;
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

namespace OrbitalSimulator.Pages
{
    /// <summary>
    /// Interaction logic for CanvasPage.xaml
    /// </summary>
    public partial class CanvasPage : BasePage<CanvasPageViewModel>
    {
        // !! Curently Loading Test Sytem in CPVM remove this later to add objects via the UI or file load
        public CanvasPage() : base(new CanvasPageViewModel(true))
        {
            InitializeComponent();
        }
    }
}