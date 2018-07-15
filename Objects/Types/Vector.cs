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
        double _Resultant;

        public Vector(double horizontalVector, double verticalVector)
        {
            _X = horizontalVector;
            _Y = verticalVector;

            //use Pythagorus to calcualte resultant
            _Resultant = Math.Sqrt((Math.Pow(_X, 2)) + (Math.Pow(_Y, 2)));
        }

        public double X { get => _X; set { _X = value; UpdateResultant(); } }
        public double Y { get => _Y; set { _Y = value; UpdateResultant(); } }
        public double Resultant { get => _Resultant; }

        private void UpdateResultant() { _Resultant = Math.Sqrt((Math.Pow(_X, 2)) + (Math.Pow(_Y, 2))); }


        public static Vector operator +(Vector V1, Vector V2)
            => new Vector(V1._X + V2._X, V1._Y + V2._Y);

        public static Vector operator *(Vector V1, double Multiplier)
            => new Vector(V1._X * Multiplier, V1._Y * Multiplier);

        public static Vector operator /(Vector V1, double Multiplier)
            => new Vector(V1._X / Multiplier, V1._Y / Multiplier);

        public override string ToString()
        {
            return Convert.ToString(Resultant);
        }
    }
}
