using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// A Class to hold Infomation for Drag Drop Objects specific too InterstellaObjects dropped onto the canvas
    /// </summary>
    public class InterstellaDragDropViewModel : DragDropObjectViewModel<InterstellaObject>
    {
        private InterstellaObjectType _Type;
        private string _TypeString = "Object Type";
        private double _Width = 40;
        private double _Height = 40;
        
        public InterstellaDragDropViewModel(InterstellaObject dataObject) : base(dataObject)
        {
            Type = dataObject.Type;
        }

        public InterstellaObjectType Type
        {
            get => _Type;
            set { _Type = value; NotifyPropertyChanged(this, nameof(Type)); }
        }

        public string TypeString { get { return _TypeString; } set { _TypeString = value; NotifyPropertyChanged(this, nameof(TypeString)); } }

        public double Width { get => _Width; set => _Width = value; }
        public double Height { get => _Height; set => _Height = value; }
    }
}
