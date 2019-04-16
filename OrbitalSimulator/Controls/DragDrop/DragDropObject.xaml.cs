using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        /// <summary>
        /// When object is picked up start a drag drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pickup(object sender, MouseButtonEventArgs e)
        {
            // Get the object's DragDrop view model
            InterstellaDragDropViewModel VM = (InterstellaDragDropViewModel)DataContext;

            // Use built in frame work to start a drag drop operation.
            DragDrop.DoDragDrop((DependencyObject)sender, VM.DataObject, DragDropEffects.Move);
        }
    }
}
