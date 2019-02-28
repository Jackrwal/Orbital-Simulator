using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class HoverDataEntryBoxViewModel : NotifyingViewModel
    {
        // ## Make Type Too String Converter?
        public InterstellaObjectType ObjectType;

        // ~~ Addd Visbility Value Converter (Visible/Hidden)
        public bool Visibility = false;

        public int Width, Height;

        public int X, Y;

        public Vector Position, Velocity;

        public InterstellaObjectViewModel DataObjectVM;

        public HoverDataEntryBoxViewModel()
        {
            Width = (int)(CanvasPageViewModel.Instance.CanvasWidth * 0.15);
            Height = (int)(CanvasPageViewModel.Instance.CanvasHeight * 0.15);
            // ~~ Set W/H relative to canvas size.
        }

        public void DisplayEntryBox(InterstellaObjectViewModel dataObjectVM)
        {
            DataObjectVM = dataObjectVM;
            Visibility = true;
        }

        public void HideBox()
        {
            Visibility = false; 
        }
    }
}
