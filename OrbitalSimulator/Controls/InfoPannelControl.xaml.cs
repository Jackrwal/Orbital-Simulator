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

// ~~ When InfoPannel Objects List Object is clicked set it to the focus on the screen

namespace OrbitalSimulator.Controls
{
    /// <summary>
    /// Interaction logic for InfoPannelControl.xaml
    /// </summary>
    public partial class InfoPannelControl : UserControl
    {
        public InfoPannelControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When an object in the InfoPannel is clicked set it to the focus of the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ## Move this to the VM with an ICOMMAND when tidying up
        private void ListObjectFocusOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            InterstellaObjectViewModel SenderVM = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;
            CanvasPageViewModel.FocusOnObject(SenderVM);
        }
    }
}
