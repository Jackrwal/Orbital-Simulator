using System;
using System.ComponentModel;

namespace OrbitalSimulator.Objects
{
    public class InterstellaObjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public InterstellaObjectType Type { get; }

        Vector Position
        {
            get { return Position; }
            set
            {
                Position = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Position)));
            }
        }

        double Radius { get; set; } = 10;
    }
}
