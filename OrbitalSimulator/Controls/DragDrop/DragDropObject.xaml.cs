using OrbitalSimulator.AttatchedProperties;
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
            InterstellaDragDropViewModel VM = (InterstellaDragDropViewModel)DataContext;
            DragDrop.DoDragDrop((DependencyObject)sender, VM.DataObject, DragDropEffects.All);
        }
    }
}
