using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// A View Model to hold a list of drag drop objects for a DragDropListControl
    /// </summary>
    // public class DragDropListViewModel<DragDropType> : NotifyingViewModel
    // where DragDropType : DragDropObjectViewModel
    public class DragDropListViewModel : NotifyingViewModel
    {
        // ## Make Generic so that it can take any Control of DragDrop Type
        private ObservableCollection<InterstellaDragDropViewModel> _DragDropObjects;

        /// <summary>
        /// Constructor too add a list of existing dragDropObjects into the DragDropObjects Collection
        /// </summary>
        /// <param name="dragDropObjects"></param>
        public DragDropListViewModel(IEnumerable<InterstellaDragDropViewModel> dragDropObjects) 
            => _DragDropObjects = new ObservableCollection<InterstellaDragDropViewModel>(dragDropObjects);
        

        public DragDropListViewModel() => _DragDropObjects = new ObservableCollection<InterstellaDragDropViewModel>();

        public ObservableCollection<InterstellaDragDropViewModel> DragDropObjects
        {
            get => _DragDropObjects;
            set { _DragDropObjects = value; NotifyPropertyChanged(this, nameof(DragDropObjects)); } 
        }


    }
}
