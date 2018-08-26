using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
{
    /// <summary>
    /// View Model for the main view of the program
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        /// <summary>
        /// The Currently Displayed Page Bound to MainWindow's content Frame
        /// </summary>
        private ApplicationPage _CurrentPage = ApplicationPage.CanvasPage;


        public WindowViewModel()
        {

        }

        public ApplicationPage CurrentPage
        {
            get { return _CurrentPage; }
            set
            {
                _CurrentPage = value;
                base.NotifyPropertyChanged(this, nameof(CurrentPage));
            }
        }

    }
}
