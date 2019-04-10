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


        // Equations Images
        public string GravitaionalEquationString { get; set; } = Equations.GravitaionLawAsBase64;
        public string NewtonsSecondLawString     { get; set; } = Equations.NewtonsSecondLawAsBase64;
        public string OrbitalVelcoityString      { get; set; } = Equations.OrbitingVelocityAsBase64;


        // Advanced Controls Properties
        static public bool UseLogSeperationScaling { get { return CanvasPageViewModel.UseLogarithmicSeperationScaling; } set { CanvasPageViewModel.UseLogarithmicSeperationScaling = value; } }

        static public bool UseLogRadiusScaling { get { return CanvasPageViewModel.UseLogarithmicRadiusScaling; } set { CanvasPageViewModel.UseLogarithmicRadiusScaling = value; } }

        static public double LinearObjectSeperation { get { return CanvasPageViewModel.LinearObjectSeperation; } set { CanvasPageViewModel.LinearObjectSeperation = value; } }

        static public double LogObjectPinch { get { return CanvasPageViewModel.LogObjectPinch; } set { CanvasPageViewModel.LogObjectPinch = value; } }
        static public double LogObjectSeperation { get { return CanvasPageViewModel.LogObjectSeperation; } set { CanvasPageViewModel.LogObjectSeperation = value; } }

        static public double LogObjectRadius { get { return CanvasPageViewModel.LogObjectRadius; } set { CanvasPageViewModel.LogObjectRadius = value; } }
        static public double LinearObjectRadius { get { return CanvasPageViewModel.LinearObjectRadius; } set { CanvasPageViewModel.LinearObjectRadius = value; } }
    }
}
