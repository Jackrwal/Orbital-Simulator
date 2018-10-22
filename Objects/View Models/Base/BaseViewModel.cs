using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrbitalSimulator_Objects
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(object sender, string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }


        
    }
}
