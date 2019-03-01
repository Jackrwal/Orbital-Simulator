using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JWOrbitalSimulatorPortable.Model;
using Point = JWOrbitalSimulatorPortable.Model.Point;

namespace OrbitalSimulator.ValueConverters
{
    public class XYMarginConverter : AbstractValueConverter<XYMarginConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point valueAsPoint = (Point)value;
            return new Thickness(valueAsPoint.X, valueAsPoint.Y, 0, 0);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
