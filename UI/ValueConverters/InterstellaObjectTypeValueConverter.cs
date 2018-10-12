using OrbitalSimulator_Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

// ## CanvasPageViewModel will contain a list of InterstellaObjectViewModels (With associated InterstellaObjects) , Bound to the view 
//    In the View the Content Presenter in the InterstellaObjectView Template will use this Value Converter to return an ellipse to present?
//    Or this could potentially just return the image brush for each plannet

namespace OrbitalSimulator_UI
{

    /// <summary>
    /// Convert a InterstellaObjectType Into a Object Skin to Fill the <see cref="Ellipse"/> object on the <see cref="CanvasPage"/>
    /// </summary>
    class InterstellaObjectTypeValueConverter : BaseValueConverter<InterstellaObjectTypeValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ## Return an ImageBrush
            switch ((InterstellaObjectType)value)
            {
                case InterstellaObjectType.Asteroid:
                    break;
                case InterstellaObjectType.Comit:
                    break;
                case InterstellaObjectType.Moon:
                    break;
                case InterstellaObjectType.DwarfPlanet:
                    break;
                case InterstellaObjectType.EarthSizedPlannet:
                    return new SolidColorBrush(Colors.Green);

                case InterstellaObjectType.GasGiant:
                    break;
                case InterstellaObjectType.Star:
                    break;
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

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
