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
    public class TrailPointPointConverter : AbstractValueConverter<TrailPointPointConverter>
    {
        /// <summary>
        ///  Takes a list of points and returns a string of these points suitable for a PolyLine's points property
        /// </summary>
        /// <param name="value">A List of points</param>
        /// <returns>A String representing a list of points</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string PointsStr = "";
            ObservableCollection<Point> Points = new ObservableCollection<Point>();

            try { Points = new ObservableCollection<Point>((IEnumerable<Point>)value); }
            catch { }

            if (Points == null || Points.Count < 1) return PointsStr;

            foreach (Point Point in Points)
            {
                PointsStr += $"{(int)Point.X},{(int)Point.Y} ";
            }

            return PointsStr.Remove(PointsStr.Count()-1, 1);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
