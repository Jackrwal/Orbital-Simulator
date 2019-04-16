using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// This is used to reformat a list of points into a string of points
    /// suitable for the points property of a WPF PolyLine
    /// </summary>
    public class TrailPointPointConverter : AbstractValueConverter<TrailPointPointConverter>
    {
        /// <summary>
        /// Takes a list of points and returns a string of these points suitable for a PolyLine's points property
        /// </summary>
        /// <param name="value">A List of points</param>
        /// <returns>A String representing a list of points</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string PointsStr = "";
            List<Point> Points = null;

            // attempt to cast the value into a collection of points
            try { Points = (List<Point>)value; }
            catch { }

            // if cast fails ( list is null ) or there are no points return the empty string.
            if (Points == null || Points.Count < 1) return PointsStr;

            // Add each point to the Points String in order
            foreach (Point Point in Points)
            {
                PointsStr += $"{(int)Point.X},{(int)Point.Y} ";
            }

            // Remove the final ',' from the string and return.
            return PointsStr.Remove(PointsStr.Count()-1, 1);
        }

        // Convert back not implimented
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
        
    }
}
