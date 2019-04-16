using System;
using System.Globalization;
using System.Windows.Media;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// Takes a string representing Colour and returns
    /// a corisponding solid colour brush
    /// </summary>
    public class RGBvalueBrushConverter : AbstractValueConverter<RGBvalueBrushConverter>
    {
        /// <summary>
        /// Takes a string for a string representing a colour and returns the
        /// corrisponding solid colour brush
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            // If conversion fails return a solid white brush
            => (new BrushConverter().ConvertFrom(value) as SolidColorBrush) ?? new SolidColorBrush(Colors.White);

        // Convert back not implimented
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }
}
