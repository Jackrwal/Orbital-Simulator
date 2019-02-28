using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
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

        public double X { get => _X; set { _X = value; } }
        public double Y { get => _Y; set { _Y = value; } }

        // Return Magnitude Using the pythagorus of the components
        public double Magnitude { get => Math.Sqrt((Math.Pow(_X, 2)) + (Math.Pow(_Y, 2))); }

        // To return the normal just swap X and Y
        public Vector Normal { get => new Vector(Y, X); }

        //Vector Addition operator
        public static Vector operator +(Vector V1, Vector V2)
            => new Vector(V1._X + V2._X, V1._Y + V2._Y);

        //Vector Subtraction operator
        public static Vector operator -(Vector V1, Vector V2)
            => new Vector(V1._X - V2._X, V1._Y - V2._Y);

        //Vector Subtract scaler
        public static Vector operator -(Vector V1, double s)
            => new Vector(V1._X - s, V1._Y - s);

        public static Vector operator +(Vector V1, double s)
            => new Vector(V1._X + s, V1._Y + s);

        //Vector mulitplication Scaler Operatoer
        public static Vector operator *(Vector V1, double Multiplier)
            => new Vector(V1._X * Multiplier, V1._Y * Multiplier);

        //Vector mulitplication Scaler Operatoer
        public static Vector operator *(double Multiplier, Vector V1)
            => new Vector(V1._X * Multiplier, V1._Y * Multiplier);


        //Vector division Scaler Operatoer
        public static Vector operator /(Vector V1, double Multiplier)
            => new Vector(V1._X / Multiplier, V1._Y / Multiplier);

        public static implicit operator Vector(Tuple<int,int> v) => new Vector(v.Item1, v.Item2);

        public override string ToString() => $"({_X}i + {_Y}j)";
    }
}
