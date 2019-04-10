using JWOrbitalSimulatorPortable.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// A Class to handle the basic functionality of dragging and dropping an object
    /// </summary>
    public class DragDropObjectViewModel<T> : NotifyingViewModel
    {
        private string _ObjectTitle;

        public DragDropObjectViewModel(T dataObject)
        {
            DataObject = dataObject;
        }

        public T DataObject { get; }
        public string ObjectTitle { get { return _ObjectTitle; } set { _ObjectTitle = value; NotifyPropertyChanged(this, nameof(ObjectTitle)); } }
    }
}
