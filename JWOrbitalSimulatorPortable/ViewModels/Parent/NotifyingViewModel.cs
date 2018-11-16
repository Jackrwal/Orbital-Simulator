using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public abstract class NotifyingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

    }
}
