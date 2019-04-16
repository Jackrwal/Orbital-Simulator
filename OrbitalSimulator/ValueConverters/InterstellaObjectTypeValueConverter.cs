using JWOrbitalSimulatorPortable.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// This converter reformats an InterstellarObjectType into a WPF ImageBrush 
    /// from the application's resources used to display that type.
    /// </summary>
    public class InterstellaObjectTypeValueConverter : AbstractValueConverter<InterstellaObjectTypeValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Try to retrieve a texture for this type
            try
            {
                switch ((InterstellaObjectType)value)
                {
                    case InterstellaObjectType.Star:
                        // return the corisponding texture from application resources
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
                // if failed to retrieve a texture return a solid white brush
                return new SolidColorBrush(Colors.White);
            }
        }

        // Convert back not implimented
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
        
    }
}
