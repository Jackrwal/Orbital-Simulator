using System;
using System.ComponentModel;

namespace OrbitalSimulator_Objects
{
    public class InterstellaObjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public InterstellaObjectType Type { get; }

        Vector _Position;

        double Radius { get; set; } = 10;

        public InterstellaObjectViewModel(InterstellaObject interstellaObject)
        {
            _Position = interstellaObject.Position;
            
        }

        public Vector Position
        {
            get => _Position;
            set
            {
                _Position = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Position)));
            } 
        }
    }
}
