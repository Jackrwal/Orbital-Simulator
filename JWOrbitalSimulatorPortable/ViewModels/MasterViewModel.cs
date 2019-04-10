using JWOrbitalSimulatorPortable.Commands;
using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class MasterViewModel : NotifyingViewModel
    {
        // ## I Am going to disable changing the size of the window untill the MasterVM and PageVMs can handle is. MainWindow.xaml L12

        public static MasterViewModel Instance;

        private ApplicationPage _CurrentPage;

        private int _WindowHeight;
        private int _WindowWidth;

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

        // This isnt being called on Start button click
        public void NavigatePage(ApplicationPage newPage)
        {
            CurrentPage = newPage;
            
            // ~~ Handling switching between open pages

        }
    }
}