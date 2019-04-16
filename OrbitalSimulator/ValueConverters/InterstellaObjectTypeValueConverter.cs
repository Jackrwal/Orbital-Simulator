using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// Convert a InterstellaObjectType Into a Object Skin to Fill the <see cref="Ellipse"/> object on the <see cref="CanvasPage"/>
    /// </summary>
    public class InterstellaObjectTypeValueConverter : AbstractValueConverter<InterstellaObjectTypeValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                switch ((InterstellaObjectType)value)
                {
                    case InterstellaObjectType.Star:
                        // Try return texture, If Resource not located break to return default texture
                         return Application.Current.Resources["ImgBrush_Star"]; 

                    case InterstellaObjectType.RockyPlanet:
                         return Application.Current.Resources["ImgBrush_Venus"]; 

                    case InterstellaObjectType.Mercury:
                         return Application.Current.Resources["ImgBrush_Mercury"]; 

                    case InterstellaObjectType.Venus:
                         return Application.Current.Resources["ImgBrush_Venus"]; 

                    case InterstellaObjectType.Moon:
                         return Application.Current.Resources["ImgBrush_Moon"]; 

                    case InterstellaObjectType.EarthSizedPlannet:
                         return Application.Current.Resources["ImgBrush_EarthlikePlannet"]; 

                    case InterstellaObjectType.Mars:
                         return Application.Current.Resources["ImgBrush_Mars"]; 

                    case InterstellaObjectType.Asteroid:
                         return Application.Current.Resources["ImgBrush_Astaroid"]; 

                    case InterstellaObjectType.GasGiant:
                         return Application.Current.Resources["ImgBrush_Jupitor"]; 
  
                    case InterstellaObjectType.Jupitor:
                         return Application.Current.Resources["ImgBrush_Jupitor"]; 

                    case InterstellaObjectType.Saturn:
                         return Application.Current.Resources["ImgBrush_Saturn"]; 

                    case InterstellaObjectType.IceGiant:
                         return Application.Current.Resources["ImgBrush_Neptune"]; 

                    case InterstellaObjectType.Neptune:
                         return Application.Current.Resources["ImgBrush_Neptune"]; 

                    case InterstellaObjectType.Uranus:
                         return Application.Current.Resources["ImgBrush_Uranus"]; 

                    case InterstellaObjectType.DwarfPlanet:
                         return Application.Current.Resources["ImgBrush_Pluto"]; 

                    default:
                        return new SolidColorBrush(Colors.White);
                }
            }
            catch (Exception)
            {
                return new SolidColorBrush(Colors.White);
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
