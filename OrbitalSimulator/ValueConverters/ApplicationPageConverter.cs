using JWOrbitalSimulatorPortable.Model;
using OrbitalSimulator.Pages;
using System;
using System.Globalization;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// Application Page Value Converter is used to load the current page into MainWindow ContentFrame
    /// </summary>
    class ApplicationPageConverter : AbstractValueConverter<ApplicationPageConverter>
    {
        /// <summary>
        /// Takes an ApplicationPage Enum value and returns an instance of the corisponding page
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                // Initialise a new page for each type of application page
                case ApplicationPage.CanvasPage:
                    return new CanvasPage();

                case ApplicationPage.StartMenu:
                    return new StartMenu();

                case ApplicationPage.LoadPage:
                    return new LoadSystemPage();

                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Convert Back is un-implimented
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
        
    }
}
