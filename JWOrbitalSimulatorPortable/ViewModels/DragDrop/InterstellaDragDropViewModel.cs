using JWOrbitalSimulatorPortable.Model;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// A Class to hold Infomation for Drag Drop Objects specific to an interstellar object
    /// </summary>
    public class InterstellaDragDropViewModel : DragDropObjectViewModel<InterstellaObject>
    {
        private InterstellaObjectType _Type;

        /// <summary>
        /// Construct a new InterstellarDragDropViewModel with a new data object
        /// and assign infomation about this object
        /// </summary>
        /// <param name="dataObject"></param>
        public InterstellaDragDropViewModel(InterstellaObject dataObject) : base(dataObject)
        {
            Type = dataObject.Type;

            ObjectTitle = dataObject.Type.ToString();
        }

        // Type used to display a texture for the object
        public InterstellaObjectType Type
        {
            get => _Type;
            set { _Type = value; NotifyPropertyChanged(this, nameof(Type)); }
        }

        // Dimensions used to display the object
        public double Width { get; set; } = 40;
        public double Height { get; set; } = 40;
    }
}