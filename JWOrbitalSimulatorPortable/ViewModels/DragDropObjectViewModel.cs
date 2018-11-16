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
        T _DataObject;

        public DragDropObjectViewModel(T dataObject)
        {
            _DataObject = dataObject;
        }

        public T DataObject  => _DataObject;
    }
}
