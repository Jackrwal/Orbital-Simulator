using System;
using System.Globalization;
using System.Windows;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// Takes a bool value and returns a WPF Visibility enum value.
    /// </summary>
    public class BoolVisiblityConverter : AbstractValueConverter<BoolVisiblityConverter>
    {
        /// <summary>
        /// Takes a boolean value and returns Visibility.Visible if its true.
        /// And Visbility.Hidden if it is false.
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            // If the Value Failes to convert to a bool (returns null) return hidden
            // Otherwise return visible if true, hidden if false
            => ((value as bool?) == null)? Visibility.Hidden : (((bool)value)? Visibility.Visible : Visibility.Collapsed);

        // Convert back not  imlpimented
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
