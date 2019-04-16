﻿using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public static class Helpers
    {
        public static double Round(this double d, int significantFigures)
        {
            if (d == 0)     
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale * Math.Round(d / scale, significantFigures);
        }

        public static Random RNG = new Random();

        public enum CanvasOrigin
        {
            TopLeft,
            BottumLeft,
            TopRight,
            BottumRight
        }

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

                // Needed a default case even though it is an enum as C# thinks not all code paths return a value
                default:
                    throw new InvalidOperationException();
            }
        }

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

                // Needed a default case even though it is an enum as C# thinks not all code paths return a value
                default:
                    throw new InvalidOperationException();

            }
        }
    }
}