using JWOrbitalSimulatorPortable.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace OrbitalSimulator.Controls
{
    /// <summary>
    /// Interaction logic for EnterSaveNameBox.xaml
    /// </summary>
    public partial class EnterSaveNameBox : UserControl
    {
        public EnterSaveNameBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When key up in the text box use content to update
        /// whether ornot the save button is enabled by the view model
        /// </summary>
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // Get the control's view model from its data context and give
            // the view model the text box's content to update if the button should be enabled
            ((EnterSaveNameBoxViewModel)DataContext).UpdateSaveButtonEnabled(((TextBox)sender).Text);
        }
    }
}
