using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using JWOrbitalSimulatorPortable.ViewModels;

namespace OrbitalSimulator.Controls
{
    /// <summary>
    /// Interaction logic for DragDropObject.xaml
    /// </summary>
    public partial class DragDropObject : UserControl
    {
        public DragDropObject()
        {
            InitializeComponent();
        }

        private void Pickup(object sender, MouseButtonEventArgs e)
        {
            // ## This hard codes DragDrop Object to being used for InterstellarObjects. 
            //    This idea of making DragDropObject Generic was for the drag drop object to work with any time of data object. 
            //    However this would require knowing the type of data object being used by the drag drop object which i never implimented
            InterstellaDragDropViewModel VM = (InterstellaDragDropViewModel)DataContext;

            DragDrop.DoDragDrop((DependencyObject)sender, VM.DataObject, DragDropEffects.Move);

            // ## Create a facsade object
        }

        private void DragDropView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            // ## Update facsade object's position to match mouse
            // Set facsade object's visiblity too false when drop is completed
        }
    }
}
