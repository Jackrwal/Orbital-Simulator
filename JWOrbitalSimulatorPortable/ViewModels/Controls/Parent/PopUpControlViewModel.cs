using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public abstract class PopUpControlViewModel : UserControlViewModel
    {
        bool _Visibility = false;

        public PopUpControlViewModel(double dimensionsToWindowSizeWighting) : base(dimensionsToWindowSizeWighting) { }

        public bool Visibility => _Visibility;

        public void Show()
        {
            _Visibility = true;
            NotifyPropertyChanged(this, nameof(Visibility));
        }

        public void Hide()
        {
            _Visibility = false;
            NotifyPropertyChanged(this, nameof(Visibility));
        }
    }
}
