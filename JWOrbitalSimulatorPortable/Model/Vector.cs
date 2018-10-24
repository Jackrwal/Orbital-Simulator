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
        double _Resultant;

        public Vector(double horizontalVector, double verticalVector)
        {
            _X = horizontalVector;
            _Y = verticalVector;

            //use Pythagorus to calcualte resultant
            _Resultant = Math.Sqrt((Math.Pow(_X, 2)) + (Math.Pow(_Y, 2)));
        }
        public Vector(Vector V)
        {
            _X = V.X;
            _Y = V.Y;

            _Resultant = V.Resultant;
        }

        public double X { get => _X; set { _X = value; UpdateResultant(); } }
        public double Y { get => _Y; set { _Y = value; UpdateResultant(); } }
        public double Resultant { get => _Resultant; }

        //use Pythagorus to calcualte resultant
        private void UpdateResultant() { _Resultant = Math.Sqrt((Math.Pow(_X, 2)) + (Math.Pow(_Y, 2))); }

        //Vector Addition operator
        public static Vector operator +(Vector V1, Vector V2)
            => new Vector(V1._X + V2._X, V1._Y + V2._Y);

        //Vector Subtraction operator
        public static Vector operator -(Vector V1, Vector V2)
            => new Vector(V1._X - V2._X, V1._Y - V2._Y);

        //Vector mulitplication Scaler Operatoer
        public static Vector operator *(Vector V1, double Multiplier)
            => new Vector(V1._X * Multiplier, V1._Y * Multiplier);

        //Vector division Scaler Operatoer
        public static Vector operator /(Vector V1, double Multiplier)
            => new Vector(V1._X / Multiplier, V1._Y / Multiplier);

        public override string ToString() => $"({_X}i + {_Y}j)";
    }
}
