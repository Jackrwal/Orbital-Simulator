using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    // This is a mostly Obselete class as later in my project, learning more about MVVM. 
    // I Decided to impliment MVVM using the approach of wrapping a logical object in a view model and refferencing the model in properties
    // Rather than ViewModels containing data about the model object which is upated when the model notifies the viewmodel a change has occured.
    // However this old system is still used in View Models i implemeneted early on like InterstellarObjectViewModel
    public abstract class NotifyingModelObject : INotifyPropertyChanged
    {
        public NotifyingViewModel ViewModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}
