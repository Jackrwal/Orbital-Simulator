using System.ComponentModel;

namespace OrbitalSimulator_Objects
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // ## Make this into a list of Model Objects to subscribe to properties of multiple model objects?
        //    Or alternativly one view model only ever binds to one item but master view models contain many view models to contain multiple objects

        protected BaseModelObject _ModelObject;

        public void SetModelObject()
        {
            _ModelObject.PropertyChanged += modelObject_PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

        public void NotifyPropertyChanged(object sender, string propertyName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        private void modelObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // When Property Is Altered in the model update the property in modelObject
        }
        
    }
}
