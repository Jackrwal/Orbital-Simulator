using JWOrbitalSimulatorPortable.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace OrbitalSimulator.Controls
{
    /// <summary>
    /// Interaction logic for HoverDataEntryBox.xaml
    /// </summary>
    public partial class HoverDataEntryBox : UserControl
    {
        public HoverDataEntryBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When Key Up in the radius text box validate text box's content
        /// </summary>
        private void RadiusBox_KeyUp(object sender, KeyEventArgs e)
        {
            // Get the text box from sender
            TextBox SenderAsTextBox = (TextBox)sender;

            // do nothing with the validated value (val) here as view model 
            // will re-validate field on setter and set the property using val
            HoverDataEntryBoxViewModel.ValidateField(SenderAsTextBox.Text, HoverDataEntryBoxViewModel.FieldType.Radius, out object val);

        }

        /// <summary>
        /// When Key Up in the mass text box validate text box's content
        /// </summary>
        private void MassBox_KeyUp(object sender, KeyEventArgs e)
        {
            // Get the text box from sender
            TextBox SenderAsTextBox = (TextBox)sender;

            // do nothing with the validated value (val) here as view model 
            // will re-validate field on setter and set the property using val
            HoverDataEntryBoxViewModel.ValidateField(SenderAsTextBox.Text, HoverDataEntryBoxViewModel.FieldType.Mass, out object val);
        }

        /// <summary>
        /// When enter key pressed pass radius text box content to view model
        /// </summary>
        private void RadiusBoxKeyDown(object sender, KeyEventArgs e)
        {
            // If Enter Key pressed
            // Set the field of the data box view model.
            // This field is validated and will update the Data Object's radius if valid
            if (e.Key == Key.Enter) CanvasPageViewModel.Instance.DataBoxVM.Radius = ((TextBox)sender).Text;

        }

        /// <summary>
        /// When enter key pressed pass mass text box content to view model
        /// </summary>
        private void MassBoxKeyDown(object sender, KeyEventArgs e)
        {
            // If Enter Key pressed
            // Set the field of the data box view model.
            // This field is validated and will update the Data Object's mass if valid
            if (e.Key == Key.Enter) CanvasPageViewModel.Instance.DataBoxVM.Mass = ((TextBox)sender).Text;
        }
    }
}