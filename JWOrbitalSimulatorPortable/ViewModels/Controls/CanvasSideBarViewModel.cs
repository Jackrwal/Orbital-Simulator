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

        public CanvasSideBarViewModel(IEnumerable<InterstellaDragDropViewModel> dragDropObjects, ICommand startSystem, ICommand stopSystem, ICommand stepSystem)
        {
            // Populate Drag Drop List
            DragDropVM = new DragDropListViewModel(dragDropObjects);

            InfoPannelVM = new InfoPannelViewModel(startSystem, stopSystem, stepSystem);
        }

        // View Model for the Canvas Side Bar Control
        // Controlls tabs and content for SideBarControl

        // Contain DragDropList VM, and get its DragDropObjects from the PageVM

        // Contain either the data for, or a VM for the Debug pannel 
        // This allows debug pannel to contain live objects from the model (Via the page VM which has access too the system)
        // And allows the Canvas Page VM to load plannets to the Debug pannel, when a specific canvas object has been clicked on 
        // (by finding the clicked data object and loading it into the debug pannel's data context here, this problebly involves the call back the ItemTemplate to get the individual data source of that object)

    }
}
