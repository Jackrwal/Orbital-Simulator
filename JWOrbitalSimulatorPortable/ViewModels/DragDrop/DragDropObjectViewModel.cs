
namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// A Class to handle the basic functionality of dragging and dropping an object
    /// </summary>
    /// <typeparam name="T">The Type of Data Object delivered on drag drop</typeparam>
    public class DragDropObjectViewModel<T> : NotifyingViewModel
    {
        // Title to display above the drag drop object
        private string _ObjectTitle;

        // Construct a drag drop object view model with a new data object
        public DragDropObjectViewModel(T dataObject)
        {
            DataObject = dataObject;
        }
        // Encapsulation of the Object's Title. And the Data Object which is transferred during drag drop
        public T DataObject { get; }
        public string ObjectTitle { get { return _ObjectTitle; } set { _ObjectTitle = value; NotifyPropertyChanged(this, nameof(ObjectTitle)); } }
    }
}
