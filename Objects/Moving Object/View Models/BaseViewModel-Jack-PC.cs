using System.ComponentModel;

namespace OSobjLibary
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

    }
}
