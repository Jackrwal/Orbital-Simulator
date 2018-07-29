using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

// ## Add a static Method to update the view model

namespace OrbitalSimulator_Objects
{
    public class InterstellaObjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        static ObservableCollection<InterstellaObjectViewModel> InterstellaObjectViewModels;

        public InterstellaObjectType Type { get; }

        Vector _Position;

        double _Radius;

        double _Width;
        double _Height;

        public InterstellaObjectViewModel(InterstellaObject interstellaObject)
        {
            _Position = interstellaObject.Position;

            // ## Set a radius inside IntersetellaObject which is based off of mass
            _Width = interstellaObject.Radius * 2;
            _Height = interstellaObject.Radius * 2;
        }

        public Vector Position
        {
            get => _Position;
            set{ _Position = value; PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Position)));} 
        }

        public double Radius {
            get => _Radius;
            set { _Radius = value; PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Radius))); }
        }

        public double Width
        {
            get => _Width;
            set { _Width = value; PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Width))); }
        }
        public double Height
        {
            get => _Height;
            set { _Height = value; PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Height))); }
        }


        public void Update()
        {

        }
    }
}
