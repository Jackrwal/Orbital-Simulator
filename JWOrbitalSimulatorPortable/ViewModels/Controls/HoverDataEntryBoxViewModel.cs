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
        public InterstellaObjectType ObjectType;

        public bool Visibility { get; set; } = false;

        public int Width { get; set; }
        public int Height { get; set; }

        public Point ScreenXY { get; set; }

        public Vector Position, Velocity;

        public InterstellaObjectViewModel DataObjectVM;

        public HoverDataEntryBoxViewModel()
        {
            // Set W/H relative to canvas size.
            Width = (int)(CanvasPageViewModel.Instance.CanvasWidth * 0.15);
            Height = (int)(CanvasPageViewModel.Instance.CanvasHeight * 0.15);
            NotifyPropertyChanged(this, nameof(Width));
            NotifyPropertyChanged(this, nameof(Height));
        }

        public void DisplayEntryBox(InterstellaObjectViewModel dataObjectVM)
        {
            DataObjectVM = dataObjectVM;
            Visibility = true;
            NotifyPropertyChanged(this, nameof(Visibility));

            ScreenXY = new Point(dataObjectVM.ScreenPosition.X+CanvasPageViewModel.Instance.SideBarWidth, dataObjectVM.ScreenPosition.Y);

            ScreenXY = new Point(dataObjectVM.ScreenPosition.X + CanvasPageViewModel.Instance.SideBarWidth + DataObjectVM.Width/2, dataObjectVM.ScreenPosition.Y + DataObjectVM.Height / 2);
            NotifyPropertyChanged(this, nameof(ScreenXY));

            // Position Text Box relative to 
        }

        public void HideBox()
        {
            Visibility = false;
            NotifyPropertyChanged(this, nameof(Visibility));
        }
    }
}
