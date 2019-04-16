using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// Impliments the INotifyPropertyChanged Interface. To be inherited by View Models 
    /// for functionality to notify view of changes to viewmodel properties
    /// </summary>
    public abstract class NotifyingViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event Rasied when a viewmodel property is changed to allow the view to update bindings
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies The View that a property has changed via the PropertyChanged event
        /// </summary>
        /// <param name="sender">The Object that's property has changed</param>
        /// <param name="propertyName">Name of the changed property</param>
        protected void NotifyPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            // If there are subscribers to property changed raise the property changed event
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            
        }

    }
}
