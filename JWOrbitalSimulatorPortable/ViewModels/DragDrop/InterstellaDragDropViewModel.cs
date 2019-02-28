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
        
        private double _Width = 40;
        private double _Height = 40;
        
        public InterstellaDragDropViewModel(InterstellaObject dataObject) : base(dataObject)
        {
            Type = dataObject.Type;

            ObjectTitle = getTypeString(dataObject);
        }

        public InterstellaObjectType Type
        {
            get => _Type;
            set { _Type = value; NotifyPropertyChanged(this, nameof(Type)); }
        }

        public double Width { get => _Width; set => _Width = value; }
        public double Height { get => _Height; set => _Height = value; }

        // If this is required elseware move it to a Value Converter
        /// <summary>
        /// Return a string for the type of an InterstellaObject
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        private string getTypeString(InterstellaObject dataObject)
        {
            switch (dataObject.Type)
            {
                case InterstellaObjectType.Asteroid:
                    break;
                case InterstellaObjectType.Comit:
                    break;
                case InterstellaObjectType.Moon:
                    return "Moon";
                case InterstellaObjectType.DwarfPlanet:
                    break;
                case InterstellaObjectType.EarthSizedPlannet:
                    return "EarthSizedPlanet";
                case InterstellaObjectType.GasGiant:
                    break;
                case InterstellaObjectType.Star:
                    return "Star"; ;
                case InterstellaObjectType.WhiteDwarf:
                    break;
                case InterstellaObjectType.NeutronStar:
                    break;
                case InterstellaObjectType.BlackHole:
                    break;
                case InterstellaObjectType.Nebula:
                    break;
                default:
                    break;
            }

            return "Invalid Type";
        }
    }
}
