using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
{
    public class Vector
    {
        double _X;
        double _Y;

        public Vector(double horizontalVector, double verticalVector)
        {
            _X = horizontalVector;
            _Y = verticalVector;
        }
        public Vector(Vector V)
        {
            _X = V.X;
            _Y = V.Y;
        }

        public double X { get => _X; set => _X = value; }
        public double Y { get => _Y; set => _Y = value; }
        public double Resultant => Math.Sqrt((Math.Pow(_X, 2)) + (Math.Pow(_Y, 2)));

        public static Vector operator +(Vector V1, Vector V2)
            => new Vector(V1._X + V2._X, V1._Y + V2._Y);

        public static Vector operator -(Vector V1, Vector V2)
            => new Vector(V1._X - V2._X, V1._Y - V2._Y);

        public static Vector operator *(Vector V1, double Multiplier)
            => new Vector(V1._X * Multiplier, V1._Y * Multiplier);

        public static Vector operator /(Vector V1, double Multiplier)
            => new Vector(V1._X / Multiplier, V1._Y / Multiplier);

        public override string ToString() => $"({_X} , {_Y})";
    }
}
