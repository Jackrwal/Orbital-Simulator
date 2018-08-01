using System.ComponentModel;

namespace OrbitalSimulator_Objects
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseModelObject _ModelObject;

        public BaseViewModel(BaseModelObject modelObject)
        {
            _ModelObject = modelObject;
            _ModelObject.PropertyChanged += modelObject_PropertyChanged;
        }

        private void modelObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // When Property Is Altered in the model update the property in modelObject
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

        public void NotifyPropertyChanged(object sender, string propertyName)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}
