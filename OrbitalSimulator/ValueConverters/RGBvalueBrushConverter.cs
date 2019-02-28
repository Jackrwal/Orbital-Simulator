using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OrbitalSimulator.ValueConverters
{
    public class RGBvalueBrushConverter : AbstractValueConverter<RGBvalueBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => (new BrushConverter().ConvertFrom(value) as SolidColorBrush) ?? new SolidColorBrush(Colors.Black);

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }
}
