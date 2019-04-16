using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OrbitalSimulator.Pages
{
    /// <summary>
    /// All Pages Inherit from AbstractMVVMPage, This ensures that every page is tied to a View Model
    /// As Every Page has a refference to a view model it allows page navigation to be tied to each Page's View Model
    /// Every ViewModel must refference the Master View Model to allow navigation between pages.
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public abstract class AbstractMVVMPage<VM> : Page
        where VM : NotifyingViewModel, new()
    {
        protected VM _VM;

        public AbstractMVVMPage(VM viewModel)
        {
            _VM = viewModel;

            DataContext = _VM;
        }

        // This is an open constructor that always creates a new VM here, This allows a blank Ctor To be used for any page
        // in the application page Value converted , instead of forcing a VM to be initialised there
        // This is potentially a clearner way to do it but i havnt decided which way to do it , probably shouldnt leave both in.
        public AbstractMVVMPage()
        {
            _VM = new VM();
            DataContext = _VM;
        }
    }
}
