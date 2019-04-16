using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// Impliments the functionality of the IValueConverter Interface to reformat a value
    /// Inherits functionality to be used in XAML Markup from MarkupExtension
    /// </summary>
    /// <typeparam name="T">The Type of converter that instance should be</typeparam>
    public abstract class AbstractValueConverter<T> :  MarkupExtension, IValueConverter
        where T : AbstractValueConverter<T>, new()
    {
        /// <summary>
        /// The Converter to provide to be Provide to XAML MarkUp
        /// </summary>
        private static T _Converter;

        /// <summary>
        /// Method to be overriden to convert a value into a different type
        /// </summary>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Method to be overriden to convert the value provided by Converter T back into its provided type  
        /// </summary>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Provide a value too the XAML MarkUp using this class.
        /// If an instance of a converter type T is provided return this else return a new instance of the type T of converter
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider) => _Converter ?? (_Converter = new T());

    }
}
