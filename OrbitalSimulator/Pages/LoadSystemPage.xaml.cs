using JWOrbitalSimulatorPortable.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OrbitalSimulator.Pages
{
    /// <summary>
    /// Interaction logic for LoadSystemPage.xaml
    /// </summary>
    public partial class LoadSystemPage : AbstractMVVMPage<LoadSystemPageViewModel>
    {
        /// <summary>
        /// Constructs a new LoadSystemPage
        /// Base constructor creates new instance of the view model
        /// </summary>
        public LoadSystemPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the item displayed for a load file is clicked load this file
        /// </summary>
        private void LoadSaveOnClick(object sender, MouseButtonEventArgs e)
        {
            // Get the save file string through the Load File View Model and load it using the page's view model
            ((LoadSystemPageViewModel)DataContext).LoadSaveFile( ((LoadFileViewModel)((Grid)sender).DataContext).SaveFileString);
        }

        /// <summary>
        /// When the delete button is clicked for a save delete the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            // Get the path of the save file from it's Load File View Model.
            // Use the page's  view model to delete the save file
            ((LoadSystemPageViewModel)DataContext).DeleteSave(((LoadFileViewModel)((Button)sender).DataContext).SaveFileString);
        }
    }
}
