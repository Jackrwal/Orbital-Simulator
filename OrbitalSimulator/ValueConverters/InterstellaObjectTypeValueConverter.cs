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
            switch ((InterstellaObjectType)value)
            {
                case InterstellaObjectType.Asteroid:
                    break;
                case InterstellaObjectType.Comit:
                    break;
                case InterstellaObjectType.Moon:

                    // Try return texture, If Resource not located break to return default texture
                    try { return Application.Current.Resources["ImgBrush_Moon"]; }
                    catch (Exception) { break; }

                case InterstellaObjectType.DwarfPlanet:
                    break;

                case InterstellaObjectType.EarthSizedPlannet:

                    // Try return texture, If Resource not located break to return default texture
                    try { return Application.Current.Resources["ImgBrush_EarthlikePlannet"]; }
                    catch (Exception) { break; }
                    
                case InterstellaObjectType.GasGiant:
                    break;

                case InterstellaObjectType.Star:

                    // Try return texture, If Resource not located break to return default texture
                    try { return Application.Current.Resources["ImgBrush_Star"]; }
                    catch (Exception) { break; }

                case InterstellaObjectType.WhiteDwarf:
                    break;

                case InterstellaObjectType.NeutronStar:
                    break;

                case InterstellaObjectType.BlackHole:
                    break;

                case InterstellaObjectType.Nebula:
                    break;

                default:
                    return new SolidColorBrush(Colors.White);
            }

            return new SolidColorBrush(Colors.White);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
