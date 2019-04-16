using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        private ApplicationPage _CurrentPage;

        public ApplicationPage CurrentPage
        {
            get => _CurrentPage;
            set
            {
                _CurrentPage = value;
                NotifyPropertyChanged(this, nameof(CurrentPage));
            }
        }

        /// <summary>
        /// This Constructor is directly called at execution to load the DataContext of the MainWindow
        /// </summary>
        public MasterViewModel()
        {
            // Use Application Page ValueConverter to load current ApplicationPage
            _CurrentPage = ApplicationPage.CanvasPage;
        }
    }
}
