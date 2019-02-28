using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace OrbitalSimulator.ValueConverters
{
    public abstract class AbstractValueConverter<T> :  MarkupExtension, IValueConverter
        where T : class, new()
    {
        /// <summary>
        /// The Converter to provide to be Provide to XAML MarkUp
        /// </summary>
        private static T _Converter;

        /// <summary>
        /// Method to be overriden to convert a value into a different type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Method to be overriden to convert the value provided by Converter T back into its provided type  
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Provide a value too the XAML MarkUp using this class.
        /// If an instance of a converter type T is provided return this else return a new instance of the type T of converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => _Converter ?? (new T());

    }
}
