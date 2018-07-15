using System.ComponentModel;

namespace OrbitalSimulator_Objects
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

    }
}
