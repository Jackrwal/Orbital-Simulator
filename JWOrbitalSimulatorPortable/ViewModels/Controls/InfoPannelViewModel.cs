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

        public InfoPannelViewModel(ICommand startSystem, ICommand stopSystem, ICommand stepSystem)
        {
            StartSystem = startSystem;
            StopSystem = stopSystem;
            StepSystem = stepSystem;
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

        //commands, ## These may later be moved into a control pannel
        public ICommand StartSystem { get; set; }
        public ICommand StopSystem { get; set; }
        public ICommand StepSystem { get; set; }
    }
}
