using JWOrbitalSimulatorPortable.Model;
using System.Collections.Generic;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// View Model Controling the Canvas Side Bar Control
    /// </summary>
    public class CanvasSideBarViewModel : NotifyingViewModel
    {
        /// <summary>
        /// DragDrop List Vm to hold all the InterstellaDragDrops to be added to the sidebar control
        /// </summary>
        public DragDropListViewModel DragDropVM { get; set; }

        /// <summary>
        /// InfoPannel View Model presents infomation about object's in the system
        /// </summary>
        public InfoPannelViewModel InfoPannelVM { get; set; }

        /// <summary>
        /// Construct the side bar view models to reflect a system and with drag drop-enabled objects.
        /// </summary>
        public CanvasSideBarViewModel(IEnumerable<InterstellaDragDropViewModel> dragDropObjects)
        {
            // Initialise the DragDropList and InfoPannel View Models
            DragDropVM = new DragDropListViewModel(dragDropObjects);
            InfoPannelVM = new InfoPannelViewModel();
        }


        // Get Equation images base64 encoded values for side bar to bind to
        public string GravitaionalEquationString  => Equations.GravitaionLawAsBase64;
        public string NewtonsSecondLawString      => Equations.NewtonsSecondLawAsBase64;
        public string OrbitalVelcoityString       => Equations.OrbitingVelocityAsBase64;


        // Advanced Controls Properties directly get and set CPVM Instance properties controling scaling.
        // Bound to by controls on the side bar.
        static public bool UseLogSeperationScaling { get { return CanvasPageViewModel.UseLogarithmicSeperationScaling; } set { CanvasPageViewModel.UseLogarithmicSeperationScaling = value; } }

        static public bool UseLogRadiusScaling { get { return CanvasPageViewModel.UseLogarithmicRadiusScaling; } set { CanvasPageViewModel.UseLogarithmicRadiusScaling = value; } }

        static public double LinearObjectSeperation { get { return CanvasPageViewModel.LinearObjectSeperation; } set { CanvasPageViewModel.LinearObjectSeperation = value; } }

        static public double LogObjectPinch { get { return CanvasPageViewModel.LogObjectPinch; } set { CanvasPageViewModel.LogObjectPinch = value; } }
        static public double LogObjectSeperation { get { return CanvasPageViewModel.LogObjectSeperation; } set { CanvasPageViewModel.LogObjectSeperation = value; } }

        static public double LogObjectRadius { get { return CanvasPageViewModel.LogObjectRadius; } set { CanvasPageViewModel.LogObjectRadius = value; } }
        static public double LinearObjectRadius { get { return CanvasPageViewModel.LinearObjectRadius; } set { CanvasPageViewModel.LinearObjectRadius = value; } }
    }
}
