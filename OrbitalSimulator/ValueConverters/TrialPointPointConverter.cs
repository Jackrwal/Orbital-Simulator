using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator.ValueConverters
{
    public class TrialPointPointConverter : AbstractValueConverter<TrialPointPointConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string PointsStr = "";
            ObservableCollection<Point> Points = new ObservableCollection<Point>();

            try { Points = new ObservableCollection<Point>((IEnumerable<Point>)value); } catch { }

            if (Points == null || Points.Count < 1) return PointsStr;

            foreach (Point Point in Points)
            {
                PointsStr += $"{(int)Point.X},{(int)Point.Y} ";
                int YpointInt = (int)Point.Y;
            }

            return PointsStr.Remove(PointsStr.Count()-1, 1);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
