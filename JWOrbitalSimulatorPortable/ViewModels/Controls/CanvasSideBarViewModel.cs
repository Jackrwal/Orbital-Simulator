using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class CanvasSideBarViewModel : NotifyingViewModel
    {
        /// <summary>
        /// DragDrop List Vm to hold all the InterstellaDragDrops to be added to the sidebar control
        /// </summary>
        public DragDropListViewModel DragDropVM { get; set; }

        public InfoPannelViewModel InfoPannelVM { get; set; }


        public CanvasSideBarViewModel(IEnumerable<InterstellaDragDropViewModel> dragDropObjects, InterstellaSystem system)
        {
            // Populate Drag Drop List
            DragDropVM = new DragDropListViewModel(dragDropObjects);

            InfoPannelVM = new InfoPannelViewModel(system);
        }
    }
}
