using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// A View Model to hold a list of drag drop objects for a DragDropListControl
    /// </summary>
    public class DragDropListViewModel : NotifyingViewModel
    {
        // List of Interstellar Drag Drop Objects contained by the control.
        private ObservableCollection<InterstellaDragDropViewModel> _DragDropObjects = new ObservableCollection<InterstellaDragDropViewModel>();

        /// <summary>
        /// Constructor too add a list of existing dragDropObjects into the DragDropObjects Collection
        /// </summary>
        /// <param name="dragDropObjects"></param>
        public DragDropListViewModel(IEnumerable<InterstellaDragDropViewModel> dragDropObjects) 
            => _DragDropObjects = new ObservableCollection<InterstellaDragDropViewModel>(dragDropObjects);
        
        // Encapsulation of Drag Drop Objects. Notify changed on collection set.
        public ObservableCollection<InterstellaDragDropViewModel> DragDropObjects
        {
            get => _DragDropObjects;
            set { _DragDropObjects = value; NotifyPropertyChanged(this, nameof(DragDropObjects)); } 
        }
    }
}
