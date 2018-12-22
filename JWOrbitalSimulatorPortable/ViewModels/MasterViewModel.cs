using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class MasterViewModel : NotifyingViewModel
    {
        private ApplicationPage _CurrentPage;

        private int _WindowHeight;
        private int _WindowWidth;

        private int _MinimumWindowHeight = 700;
        private int _MinimumWindowWidth = 1200;

        /// <summary>
        /// This Constructor is directly called at execution to load the DataContext of the MainWindow
        /// </summary>
        public MasterViewModel()
        {
            // Use Application Page ValueConverter to load current ApplicationPage
            _CurrentPage = ApplicationPage.CanvasPage;

            _WindowHeight = _MinimumWindowHeight;
            _WindowWidth = _MinimumWindowWidth;
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
                if (value < _MinimumWindowHeight) return;
                _WindowHeight = value;
                NotifyPropertyChanged(this, nameof(WindowHeight));
            }
        }
        public int WindowWidth
        {
            get => _WindowWidth;
            set
            {
                if (value < _MinimumWindowWidth) return;
                _WindowWidth = value;
                NotifyPropertyChanged(this, nameof(WindowWidth));
            }
        }
    }
}
