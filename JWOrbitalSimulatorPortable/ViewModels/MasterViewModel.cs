using JWOrbitalSimulatorPortable.Model;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// Controls the application. 
    /// Presents the model to the view to be displayed using the view models.
    /// </summary>
    public class MasterViewModel : NotifyingViewModel
    {
        // The most up to date instance of the master view model
        public static MasterViewModel Instance;

        //The Page of the application that is currently loaded to the window
        private ApplicationPage _CurrentPage;

        // The Current W and H of the window
        private int _WindowHeight;
        private int _WindowWidth;

        // The Miniumum hight the window can be
        private int _MinimumWindowHeight = 700;
        private int _MinimumWindowWidth = 1300;

        /// <summary>
        /// This Constructor is directly called at execution to load the DataContext of the MainWindow
        /// </summary>
        public MasterViewModel()
        {
            // Use Application Page ValueConverter to load current ApplicationPage
            _CurrentPage = ApplicationPage.StartMenu;

            _WindowHeight = _MinimumWindowHeight;
            _WindowWidth = _MinimumWindowWidth;

            // The Static Instance of the View Model that controls this application
            Instance = this;
        }

        // Encapsulate fields as properties and notify the view when they are changed
        public ApplicationPage CurrentPage
        {
            get => _CurrentPage;
            set
            {
                _CurrentPage = value;
                NotifyPropertyChanged(this, nameof(CurrentPage));
            }
        }

        public int WindowHeight
        {
            get => _WindowHeight;
            set
            {
                _WindowHeight = value;
                NotifyPropertyChanged(this, nameof(WindowHeight));
            }
        }
        public int WindowWidth
        {
            get => _WindowWidth;
            set
            {
                _WindowWidth = value;
                NotifyPropertyChanged(this, nameof(WindowWidth));
            }
        }

        public void NavigatePage(ApplicationPage newPage)
        {
            CurrentPage = newPage;
        }
    }
}