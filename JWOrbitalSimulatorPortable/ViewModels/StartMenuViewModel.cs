using JWOrbitalSimulatorPortable.Commands;
using JWOrbitalSimulatorPortable.Model;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// Controls the Start Menu View Model
    /// allows the user to navigate the application from a main menu.
    /// </summary>
    public class StartMenuViewModel : NotifyingViewModel
    {
        public StartMenuViewModel()
        {
            // Set the relay commands to execute appropriate view model methods on button press
            NewSystem = new RelayCommand(loadNewSystem);
            Demo = new RelayCommand(loadDemo);
            LoadSystem = new RelayCommand(openLoadMenu);
        }

        /// <summary>
        /// Open a new Load System Page
        /// </summary>
        private void openLoadMenu()
        {
            // Navigate to load system page
            MasterViewModel.Instance.NavigatePage(ApplicationPage.LoadPage);
        }

        /// <summary>
        ///  Open a new CanvasPage and load a blank system
        /// </summary>
        private void loadNewSystem()
        {
            // Navigate to canvas page and load a blank system
            MasterViewModel.Instance.NavigatePage(ApplicationPage.CanvasPage);
            CanvasPageViewModel.Instance.LoadNewSystem();
        }

        /// <summary>
        /// Load the demonstration system
        /// </summary>
        private void loadDemo()
        {
            // navigate to the canvas page
            MasterViewModel.Instance.NavigatePage(ApplicationPage.CanvasPage);

            // Load an instance of the Demonstration System
            CanvasPageViewModel.Instance.LoadSystem(DemoSystemModel.GetDemoInstance);
        }

        // The relay commands that are executed by button presses
        public ICommand Demo { get; set; }
        public ICommand NewSystem { get; set; }
        public ICommand LoadSystem { get; set; }
    }
}
