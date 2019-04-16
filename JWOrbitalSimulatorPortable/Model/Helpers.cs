using JWOrbitalSimulatorPortable.ViewModels;
using System;

namespace JWOrbitalSimulatorPortable.Model
{
    /// <summary>
    /// Contains Misc Helper methods that are used to perform generic tasks throughout the solution
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Return this double rounded to a given number of significant figures
        /// Used as (some double).Round(2)
        /// </summary>
        public static double Round(this double d, int significantFigures)
        {
            if (d == 0)     
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, significantFigures);
        }

        // Random Number Generator
        public static Random RNG = new Random();

        /// <summary>
        /// Enum used with canvas centralize and PointFromRelativeOrigin Methods
        /// Represents a corner of the canvas which co-ordinate is relative to
        /// </summary>
        public enum CanvasOrigin
        {
            TopLeft,
            BottumLeft,
            TopRight,
            BottumRight
        }

        /// <summary>
        /// Takes a co-ordinate relative to a corner of the canvas and returns 
        /// the co-ordinate relative to the centre of the canvas
        /// </summary>
        public static Vector Centrlize(Vector Position, CanvasOrigin Origin)
        {
            int CanvasWidth = (int)CanvasPageViewModel.Instance?.CanvasWidth;
            int CanvasHeight = (int)CanvasPageViewModel.Instance?.CanvasHeight;

            switch (Origin)
            {
                case CanvasOrigin.TopLeft:
                    return new Vector(Position.X - (CanvasWidth / 2), -Position.Y + (CanvasHeight / 2));

                case CanvasOrigin.BottumLeft:
                    return new Vector(Position.X - (CanvasWidth / 2), Position.Y + (CanvasHeight / 2));

                case CanvasOrigin.TopRight:
                    throw new NotImplementedException();

                case CanvasOrigin.BottumRight:
                    throw new NotImplementedException();

                // Needs a default case to avoid not all code paths return a value error
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Take a co-ordinate relative to the center of the canvas and returns it relative to a
        /// CanvasOrigin
        /// </summary>
        public static Vector PointFromRelativeOrigin(Vector Position, CanvasOrigin Origin)
        {
            int CanvasWidth = (int)CanvasPageViewModel.Instance?.CanvasWidth;
            int CanvasHeight = (int)CanvasPageViewModel.Instance?.CanvasHeight;

            switch (Origin)
            {
                case CanvasOrigin.TopLeft:
                    return new Vector(Position.X + (CanvasWidth / 2), -Position.Y + (CanvasHeight / 2));

                case CanvasOrigin.BottumLeft:
                    return new Vector(Position.X + (CanvasWidth / 2), Position.Y - (CanvasHeight / 2));

                case CanvasOrigin.TopRight:
                    throw new NotImplementedException();

                case CanvasOrigin.BottumRight:
                    throw new NotImplementedException();

                // Needs a default case to avoid not all code paths return a value error
                default:
                    throw new InvalidOperationException();

            }
        }
    }
}
