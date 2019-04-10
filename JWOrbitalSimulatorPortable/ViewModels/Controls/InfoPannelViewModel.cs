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
        private ObservableCollection<InterstellaObjectViewModel> _InfoObjects;
        private InterstellaSystem _System;

        public InfoPannelViewModel(InterstellaSystem system)
        {
            _System = system;

            _InfoObjects = new ObservableCollection<InterstellaObjectViewModel>(system.InterstellaObjects.Select( (Object) => new InterstellaObjectViewModel(Object)));
        }

        public void AddDisplayObject(InterstellaObjectViewModel newObjectToDisplay)
        {
            if(!_InfoObjects.Contains(newObjectToDisplay)) _InfoObjects.Add(newObjectToDisplay);
            NotifyPropertyChanged(this, nameof(InfoObjects));
        }

        public ObservableCollection<InterstellaObjectViewModel> InfoObjects
        {
            // Could have done this setter by doing a select on _System.InterstellarObjects so that it always reflects the system objects?
            // This also eliminates the need to add or remove display objects as just required a notification that the InfoObject have changed on system.CollectionAltered
            get { return _InfoObjects; }
            set { _InfoObjects = value; NotifyPropertyChanged(this, nameof(InfoObjects)); }
        }

        // ## Wanted to display system speed and zoom on the info pannel however could not make work in time.
    }
}
