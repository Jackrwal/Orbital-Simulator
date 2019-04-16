using JWOrbitalSimulatorPortable.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

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
        private void ListObjectFocusOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the sending object's view model from it's data context
            InterstellaObjectViewModel SenderVM = ((Ellipse)sender).DataContext as InterstellaObjectViewModel;

            // Set the view canvas page to focus on this object
            CanvasPageViewModel.FocusOnObject(SenderVM);
        }
    }
}
