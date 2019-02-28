using JWOrbitalSimulatorPortable.Commands;
using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class InfoPannelViewModel : NotifyingViewModel
    {
        private ObservableCollection<InterstellaObjectViewModel> _InfoObjects = new ObservableCollection<InterstellaObjectViewModel>();
        private InterstellaSystem _System;

        public InfoPannelViewModel(InterstellaSystem system)
        {
            StepSystem = new RelayCommand(system.Step);
            _System = system;
        }

        public void AddDisplayObject(InterstellaObjectViewModel newObjectToDisplay)
        {
            if(!_InfoObjects.Contains(newObjectToDisplay)) _InfoObjects.Add(newObjectToDisplay);
        }

        public ObservableCollection<InterstellaObjectViewModel> InfoObjects
        {
            get { return _InfoObjects; }
            set { _InfoObjects = value; NotifyPropertyChanged(this, nameof(InfoObjects)); }
        }

        public RelayCommand StepSystem { get; set; }


        // ## Implement these for System Infomation Text Boxes when easy to do
        //public double Zoom => CanvasPageViewModel.MasterScale;
        //public double SystemSpeed => Math.Round(Parent.Parent.SystemSpeed, 1);

    }
}
