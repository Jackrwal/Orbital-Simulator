using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private ICommand _Navigate;

        public WindowViewModel()
        {
            _Navigate = new ParamRelayCommand(new Action<object>(Switchpage));
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


        private void Switchpage(object newPage)
        {
            // Needs to take a page to navigate too, This requires a different Action inside the command so may require a new Command for navigating
            throw new NotImplementedException("Page Switching Not Yet Implimented (WindowViewModel.cs L42)");

        }

    }
}
