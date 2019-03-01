using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrbitalSimulator.ValueConverters
{
    public class BoolVisiblityConverter : AbstractValueConverter<BoolVisiblityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            ((bool)value) ? Visibility.Visible : Visibility.Hidden;

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
