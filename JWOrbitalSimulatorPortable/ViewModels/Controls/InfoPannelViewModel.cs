using JWOrbitalSimulatorPortable.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// View Model Controls the InfoPannel Control
    /// </summary>
    public class InfoPannelViewModel : NotifyingViewModel
    {
        // Collection of objects to display infomation about
        // Always reflects the CPVM's system.
        private InterstellaSystem _System => CanvasPageViewModel.Instance.System;

        // called by CPVM when the system page has been altered
        /// <summary>
        /// Notify the view that the objects to display infomation about has changed
        /// </summary>
        public void onSystemAltered()
            => NotifyPropertyChanged(this, nameof(InfoObjects));

        // The Set of view models to display infomation from to the info pannel
        public ObservableCollection<InterstellaObjectViewModel> InfoObjects
            => new ObservableCollection<InterstellaObjectViewModel>(_System.InterstellaObjects.Select((Object) => new InterstellaObjectViewModel(Object)));
    }
}
