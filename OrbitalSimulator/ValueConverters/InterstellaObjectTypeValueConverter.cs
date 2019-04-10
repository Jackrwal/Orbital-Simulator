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
                case InterstellaObjectType.Star:
                    // Try return texture, If Resource not located break to return default texture
                    try { return Application.Current.Resources["ImgBrush_Star"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.RockyPlanet:
                    try { return Application.Current.Resources["ImgBrush_Venus"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Mercury:
                    try { return Application.Current.Resources["ImgBrush_Mercury"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Venus:
                    try { return Application.Current.Resources["ImgBrush_Venus"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Moon:
                    try { return Application.Current.Resources["ImgBrush_Moon"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.EarthSizedPlannet:
                    try { return Application.Current.Resources["ImgBrush_EarthlikePlannet"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Mars:
                    try { return Application.Current.Resources["ImgBrush_Mars"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Asteroid:
                    try { return Application.Current.Resources["ImgBrush_Astaroid"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.GasGiant:
                    try { return Application.Current.Resources["ImgBrush_Jupitor"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Jupitor:
                    try { return Application.Current.Resources["ImgBrush_Jupitor"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Saturn:
                    try { return Application.Current.Resources["ImgBrush_Saturn"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.IceGiant:
                    try { return Application.Current.Resources["ImgBrush_Neptune"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Neptune:
                    try { return Application.Current.Resources["ImgBrush_Neptune"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.Uranus:
                    try { return Application.Current.Resources["ImgBrush_Uranus"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.DwarfPlanet:
                    try { return Application.Current.Resources["ImgBrush_Pluto"]; }
                    catch (Exception) { return new ImageBrush(); }

                case InterstellaObjectType.WhiteDwarf:
                    break;
                case InterstellaObjectType.NeutronStar:
                    break;
                case InterstellaObjectType.Comit:
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
