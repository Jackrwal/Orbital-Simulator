using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
{
    public abstract class BaseModelObject : INotifyPropertyChanged
    {
        // ## This Base Class should subscribe to the Propety Changed event of the ViewModel

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void NotifyPropertyChanged(object sender, string propertyName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}
