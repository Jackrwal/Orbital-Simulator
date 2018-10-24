using JWOrbitalSimulatorPortable.Model;
using OrbitalSimulator.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// Application Page Value Converter is used to load the current page into MainWindow ContentFrame
    /// </summary>
    class ApplicationPageConverter : BaseValueConverter<ApplicationPageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                // Initialise a blank Canvas Page
                case ApplicationPage.CanvasPage:
                    return new CanvasPage();

                case ApplicationPage.StartMenu:
                    return new StartMenu();

                default:
                    throw new InvalidOperationException();
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
